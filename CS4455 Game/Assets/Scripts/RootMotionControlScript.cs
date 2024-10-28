using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CharacterInputController))]
public class RootMotionControlScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

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
        originalMovementSpeed = rootMovementSpeed; // Keep Original Speed
        originalTurnSpeed = rootTurnSpeed;

    }

    void FixedUpdate()
    {
        // Apply the filtered input to the animator's blend tree parameters
        anim.SetFloat("velx", cinput.Turn);
        anim.SetFloat("vely", cinput.Forward);

        // Check if the cat is moving forward
        bool isMovingForward = cinput.Forward > 0.1f;

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
    }

    // activate speed boost
    public void ActivateSpeedBoost(float duration)
    {
        if (!speedBoostActive)  
        {
            StartCoroutine(SpeedBoostRoutine(duration));
        }
    }

    private IEnumerator SpeedBoostRoutine(float duration)
    {
        speedBoostActive = true;
        rootMovementSpeed *= speedMultiplier;
        rootTurnSpeed *= speedMultiplier;
        Debug.Log("Speed boost activated!");

        yield return new WaitForSeconds(duration);  

        rootMovementSpeed = originalMovementSpeed;
        rootTurnSpeed = originalTurnSpeed;
        speedBoostActive = false;
        Debug.Log("Speed boost deactivated.");
    }
}
