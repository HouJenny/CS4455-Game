using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletableT : MonoBehaviour
{
    private TCounterUI tCounter;
    public AudioClip collectionSound; // Reference to the Audio Clip

    public AudioSource audioSource; // Reference to the Audio Source

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
            audioSource.Play();
            // 销毁当前的 T
            if (c.CompareTag("Player"))
            {
                Debug.Log("Player collected the item!");

                // Create a temporary object to play the sound
                GameObject tempAudio = new GameObject("TempAudio");
                AudioSource tempSource = tempAudio.AddComponent<AudioSource>();

                tempSource.clip = collectionSound;
                tempSource.Play();

                Destroy(tempAudio, collectionSound.length); // Clean up
            }
            Destroy(gameObject);
        }
    }
    

