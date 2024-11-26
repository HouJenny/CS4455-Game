using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScooterController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float turnSpeed = 100f; // Turning speed
    public Vector3 newCenterOfMass; 

    private float speedBoostTime = 10.0f;
    private float currTime;
    private float boostEnd;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = newCenterOfMass;
        
    }
    void Awake() {
        currTime = Time.realtimeSinceStartup;
        boostEnd = Time.realtimeSinceStartup;
    }
    void Update()
    {
        // Get input for movement and turning
        float moveVertical = Input.GetAxis("Vertical"); // Forward/backward
        float moveHorizontal = Input.GetAxis("Horizontal"); // Turning left/right

        // Move the scooter forward/backward based on its local space
        transform.Translate(Vector3.right * moveVertical * speed * Time.deltaTime);

        // Rotate the scooter left/right based on the horizontal input
        transform.Rotate(Vector3.up, moveHorizontal * turnSpeed * Time.deltaTime);
    }

    public void SpeedBoost() {
        currTime = Time.realtimeSinceStartup;
        boostEnd = Time.realtimeSinceStartup + speedBoostTime;
        speed = 8f;
        while (currTime < boostEnd) {
            currTime = Time.realtimeSinceStartup;
        }
        speed = 5f;
    }
    

}
