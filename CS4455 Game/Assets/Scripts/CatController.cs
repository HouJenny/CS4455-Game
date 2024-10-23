using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatController : MonoBehaviour
{
 // Rigidbody of the player.
 private Rigidbody rb; 

 // Movement along X and Y axes.
 private float movementX;
 private float movementY;

 // Speed at which the player moves.
 public float speed = 0; 
 public float terminalVelocity = 20f;

 // Start is called before the first frame update.
 void Start()
    {
 // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
    }
 
 // This function is called when a move input is detected.
 void OnMove(InputValue movementValue)
    {
 // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

 // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

 // FixedUpdate is called once per fixed frame-rate frame.
 private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        
        
        rb.AddForce(movement * speed);
        LimitVelocity();



    }
     private void LimitVelocity()
     {
         // Get the current velocity.
         Vector3 velocity = rb.velocity;

         // Check if the current velocity magnitude exceeds the terminal velocity.
         if (velocity.magnitude > terminalVelocity)
         {
             // Clamp the velocity to the terminal velocity.
             rb.velocity = velocity.normalized * terminalVelocity;
         }
     }

}