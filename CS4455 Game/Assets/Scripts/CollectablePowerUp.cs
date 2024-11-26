using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePowerUp : MonoBehaviour
{
    public AudioSource audioSource;
    private bool hasCollected = false;
    private bool alreadyPlayed = false;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    void OnTriggerEnter(Collider c)
    {   
        
        if (c.CompareTag("Player") && !hasCollected)
        {
            hasCollected = true;
            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
            mr.enabled = false;
            }
            if (hasCollected & !alreadyPlayed) {
                Debug.Log("playing sound");
                PlaySound();
                alreadyPlayed = true;
            }
        }
    }

    private void PlaySound() {
        if (!audioSource.isPlaying) // Ensure it doesn't overlap.
        {
            audioSource.Play();
        }
    }

}
