using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScooterInteraction : MonoBehaviour
{
    public GameObject cat;
    public GameObject scooter;
    public ScooterController scooterController;
    public CharacterInputController catController;
    public RootMotionControlScript rootMotionControl; // Reference to RootMotionControlScript
    
    private Animator catAnimator;   

    private bool isNearScooter = false;
    private bool isOnScooter = false;

    void Start()
    {
        // Ensure both controllers are in the correct state at the start
        scooterController.enabled = false;
        rootMotionControl.enabled = true;
        
        catAnimator = cat.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == cat)
        {
            isNearScooter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == cat)
        {
            isNearScooter = false;
        }
    }

    void Update()
    {
        // Mount the scooter
        if (isNearScooter && !isOnScooter && Input.GetKeyDown(KeyCode.F))
        {
            scooterController.enabled = true;
            rootMotionControl.enabled = false; // Disable root motion control
            isOnScooter = true;
            
            // Stop the cat's movement and switch to idle animation
            StopCatMovement();

            // Parent the cat to the scooter and set position
            cat.transform.SetParent(scooter.transform);
            cat.transform.localPosition = new Vector3(-.1f, .2f, 0f);
            cat.transform.localRotation = Quaternion.Euler(0,90,0);
            // Make the cat's Rigidbody kinematic to stop physics interactions

            

            Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();
            if (catRigidbody != null)
            {
                catRigidbody.isKinematic = true;
            }
        }

        // Dismount the scooter
        if (isOnScooter && Input.GetKey(KeyCode.Escape))
        {
            scooterController.enabled = false;
            rootMotionControl.enabled = true; // Re-enable root motion control
            isOnScooter = false;

            // Unparent the cat from the scooter
            cat.transform.SetParent(null);

            // Re-enable physics for the cat's Rigidbody
            Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();
            if (catRigidbody != null)
            {
                catRigidbody.isKinematic = false;
            }
        }
    }
    
    private void StopCatMovement()
    {
        // Set Animator parameters to zero to stop the cat's movement
        if (catAnimator != null)
        {
            catAnimator.SetFloat("velx", 0);
            catAnimator.SetFloat("vely", 0);
        }

        // Stop any forward velocity in the CharacterInputController if needed
        if (catController != null)
        {
            catController.Forward = 0;
            catController.Turn = 0;
        }
    }
}
