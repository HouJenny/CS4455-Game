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

    private float originalMovementSpeed;  // Original Moving Speed
    private float speedMultiplier = 2f;   // Speed Multiplier
    private bool speedBoostActive = false;  // Check if in the speedup mode

    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        cinput = GetComponent<CharacterInputController>();

        originalMovementSpeed = rootMovementSpeed; // Keep Original Speed
    }

    void FixedUpdate()
    {
        // Apply the filtered input to the animator's blend tree parameters
        anim.SetFloat("velx", cinput.Turn);
        anim.SetFloat("vely", cinput.Forward);

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
        Debug.Log("Speed boost activated!");

        yield return new WaitForSeconds(duration);  

        rootMovementSpeed = originalMovementSpeed;  
        speedBoostActive = false;
        Debug.Log("Speed boost deactivated.");
    }
}