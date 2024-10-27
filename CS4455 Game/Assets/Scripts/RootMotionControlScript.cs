using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CharacterInputController))]
public class RootMotionControlScript : MonoBehaviour {
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        cinput = GetComponent<CharacterInputController>();
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
}