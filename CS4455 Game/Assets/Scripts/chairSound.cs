using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairSound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the Audio Source

    void OnTriggerEnter(Collider other)
    {
        // Check if the player interacts with the item
        if (other.CompareTag("Scooter") || other.CompareTag("Player"))
        {
            Debug.Log("hit Chair");
            if (!audioSource.isPlaying) {
                audioSource.Play(); // Play the sound effect
            }
            
        }
    }
}
