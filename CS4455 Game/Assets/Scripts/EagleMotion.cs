using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMotion : MonoBehaviour
{
    public bool startFlying = false; 
    public Animator eagleAnimator; 
    public float flyRange = 3f;   
    public Transform cat;       
    private bool isCatNear = false; 
    public float flySpeed = 1f;    
    public float maxHeight = 15f;
    void Start()
    {
        if (startFlying)
        {
            eagleAnimator.SetBool("isCatNear", false);
            eagleAnimator.SetBool("isFlying", true);
        }
    }

    void Update()
    {
        // Check the distance between the cat and the eagle
        if (Vector3.Distance(transform.position, cat.position) <= flyRange)
        {
            if (!isCatNear) // If the cat wasn't already near and the eagle hasn't flown yet
            {
                isCatNear = true;
                FlyAway();
            }
        }
    }

    // Method to trigger the eagle's flying animation
    void FlyAway()
    {
        // Set the 'isFlying' parameter in the animator to true
        eagleAnimator.SetBool("isFlying", true);

        eagleAnimator.SetBool("isCatNear", false);
        
        // Optionally, you can also add movement to make the eagle fly away
        // Example: Move the eagle upwards or in a random direction
        StartCoroutine(FlyAndLeave());
    }

    // Coroutine to make the eagle fly and leave the area
    private IEnumerator FlyAndLeave()
    {
        Vector3 startPosition = transform.position;

        // Gradually increase the Y position of the eagle over time
        float elapsedTime = 0f;
        while (transform.position.y < maxHeight)
        {
            elapsedTime += Time.deltaTime * flySpeed;

            // Lerp the Y position to gradually increase it
            float newY = Mathf.Lerp(startPosition.y, maxHeight, elapsedTime);

            // Update the eagle's position, keeping the X and Z the same
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null; // Continue the movement in the next frame
        }
    }
}