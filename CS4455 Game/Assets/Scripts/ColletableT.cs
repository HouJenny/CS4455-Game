using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletableT : MonoBehaviour
{
    private TCounterUI tCounter;
    private bool hasCollected = false;

    public AudioSource audioSource;
    private bool alreadyPlayed = false;


    void Start()
    {
        // Find the TIconCounterUI script in the scene
        tCounter = FindObjectOfType<TCounterUI>();
        audioSource = GetComponent<AudioSource>();
        
    }


    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player") && !hasCollected)
        {
            hasCollected = true;
            GameManager.Instance.UpdateCollectiblesUI();
            Debug.Log("hit T");

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
