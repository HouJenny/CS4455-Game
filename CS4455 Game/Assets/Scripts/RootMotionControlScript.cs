using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CharacterInputController))]
public class RootMotionControlScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    public GameObject scooterPressStandingSpot;
    public float scooterCloseEnoughForMatchDistance = 2f;
    public float scooterCloseEnoughForPressDistance = 0.22f;
    public float scooterCloseEnoughForPressAngleDegrees = 5f;
    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;

	public bool catIdle = false;
    bool _inputActionFired = false;
    private bool isAttacking = false;


    //Useful if you implement jump in the future...
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;

    private AudioSource movementAudio;      // AudioSource for cat movement
    private float originalMovementSpeed;  // Original Moving Speed
    private float speedMultiplier = 2f;   // Speed Multiplier
    private bool speedBoostActive = false;  // Check if in the speedup mode

    private float originalTurnSpeed;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        cinput = GetComponent<CharacterInputController>();

        // Get the two AudioSource components attached to the cat
        AudioSource[] audioSources = GetComponents<AudioSource>();
        movementAudio = audioSources[2];
        originalMovementSpeed = animationSpeed; // Keep Original Speed
    }

    private void Update()
    {
        if (cinput.enabled)
        {
            if (cinput.Attack)
            {
                isAttacking = true; // Set attacking state
            }
            // Note that we don't overwrite a true value already stored
            // Is only cleared to false in FixedUpdate()
            // This makes certain that the action is handled!
            _inputActionFired = _inputActionFired || cinput.Action;

        }
        originalTurnSpeed = rootTurnSpeed;

    }

    void FixedUpdate()
    {

        bool getOnScooter = false;
        bool doMatchToScooter = false;

        anim.speed = animationSpeed;
        // Apply the filtered input to the animator's blend tree parameters
        anim.SetFloat("velx", cinput.Turn);
        anim.SetFloat("vely", cinput.Forward);

        // Check if the cat is moving forward
        bool isMovingForward = cinput.Forward > 0.1f;

		if (catIdle) {
			anim.SetBool("isForward", false);
		}
		else {
			anim.SetBool("isForward", isMovingForward);
		}

        // Ground checking (assuming you're using a ground-checking method)
        bool isGrounded = true; // Replace with actual ground check

        // Apply root motion if grounded
        if (isGrounded)
        {
            Vector3 rootPosition = anim.rootPosition;
            Quaternion rootRotation = anim.rootRotation;

            // Interpolate for smooth root movement and rotation
            rootPosition = Vector3.LerpUnclamped(transform.position, rootPosition, rootMovementSpeed);
            rootRotation = Quaternion.LerpUnclamped(transform.rotation, rootRotation, rootTurnSpeed);

            rbody.MovePosition(rootPosition);
            rbody.MoveRotation(rootRotation);
        }

        // Play the movement sound if moving forward
        if (isMovingForward && isGrounded)
        {            
            if (!movementAudio.isPlaying)
            {
                movementAudio.Play();
            }
        }
        else
        {
            // Stop the movement sound if not moving forward
            if (movementAudio.isPlaying)
            {
                movementAudio.Stop();
            }
        }

        float scooterDistance = float.MaxValue;
        float scooterAngleDegrees = float.MaxValue;

        if (scooterPressStandingSpot != null)
        {
            scooterDistance = Vector3.Distance(transform.position, scooterPressStandingSpot.transform.position);
            scooterAngleDegrees = Quaternion.Angle(transform.rotation, scooterPressStandingSpot.transform.rotation);
        }

        if(_inputActionFired)
        {
            _inputActionFired = false; // clear the input event that came from Update()

            Debug.Log("Action pressed");

            if (scooterDistance <= scooterCloseEnoughForMatchDistance)
            {
                if(scooterDistance <= scooterCloseEnoughForPressDistance &&
                    scooterAngleDegrees <= scooterCloseEnoughForPressAngleDegrees)
                {
                    Debug.Log("Scooter press initiated");

                    getOnScooter = true;
                    
                }
                else
                {
                    // TODO UNCOMMENT THESE LINES FOR TARGET MATCHING
                    Debug.Log("Match to scooter initiated");
                    doMatchToScooter = true;
                }

            }
        }

        // get info about current animation 
        var animState = anim.GetCurrentAnimatorStateInfo(0); 
        // If the transition to scooter press has been initiated then we want  
        // to correct the character position to the correct place 
        if ( animState.IsName("Get On Scooter")  
        && !anim.IsInTransition(0) && !anim.isMatchingTarget ) 
        {
            if (scooterPressStandingSpot != null) 
                {
                    Debug.Log("Target matching correction started"); 
                    initalMatchTargetsAnimTime = animState.normalizedTime; 
                    var t = scooterPressStandingSpot.transform; 
                    anim.MatchTarget(t.position, t.rotation, AvatarTarget.Root,  
                    new MatchTargetWeightMask(new Vector3(1f, 0f, 1f), 
                    1f),  
                    initalMatchTargetsAnimTime, 
                    exitMatchTargetsAnimTime);
                }
        }

        if (isAttacking)
        {
            anim.SetBool("isAttacking", true); // Trigger attack animation
            isAttacking = false; // Reset attack state after triggering
        }
        else
        {
            anim.SetBool("isAttacking", false); // Reset if not attacking
        }

        anim.SetBool("getOnScooter", getOnScooter);
        anim.SetBool("matchToScooter", doMatchToScooter);

        LimitVelocity();
    }

    // activate speed boost
    public void ActivateSpeedBoost(float duration)
    {
        if (!speedBoostActive)  
        {
            StartCoroutine(SpeedBoostRoutine(duration));
        }
    }

    private void LimitVelocity() {
        Vector3 velocity = rbody.velocity;
        if (velocity.magnitude > cinput.terminalVelocity) {
            rbody.velocity = velocity.normalized * cinput.terminalVelocity;
        }
    }

    public GameObject scooterObject;

    private void OnAnimatorIK(int layerIndex)
    {
        if(anim) { 
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0); 
            if(astate.IsName("Get On Scooter"))
            {
                float scooterWeight = anim.GetFloat("scooterClose");
                // Set the look target position, if one has been assigned 
                if(scooterObject != null)
                { 
                    anim.SetLookAtWeight(scooterWeight); 
                    anim.SetLookAtPosition(scooterObject.transform.position); 
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand,scooterWeight); 
                    anim.SetIKPosition(AvatarIKGoal.RightHand, 
                    scooterObject.transform.position);  
                } 
            } 
            else  
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand,0); 
                anim.SetLookAtWeight(0); 
            } 
        } 
    }

    private IEnumerator SpeedBoostRoutine(float duration)
    {
        speedBoostActive = true;
        animationSpeed *= speedMultiplier;
        Debug.Log("Speed boost activated!");

        yield return new WaitForSeconds(duration);

        animationSpeed = originalMovementSpeed;
        speedBoostActive = false;
        Debug.Log("Speed boost deactivated.");
    }
}
