using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairSound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the Audio Source
    public AudioClip collectionSound; // Reference to the Audio Clip


    void OnTriggerEnter(Collider other)
    {
        // Check if the player interacts with the item
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit Chair");
            audioSource.Play(); // Play the sound effect
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();

            tempSource.clip = collectionSound;
            tempSource.Play();

            Destroy(tempAudio, collectionSound.length); // Clean up

        }
    }
}
