using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the Audio Source
    public AudioClip collectionSound; // Reference to the Audio Clip


    void OnTriggerEnter(Collider other)
    {
        // Check if the player interacts with the item
        if (other.CompareTag("Player"))
        {
            // Play the sound effect
            audioSource.Play();
        }
    }

}