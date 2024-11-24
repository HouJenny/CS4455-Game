using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletableT : MonoBehaviour
{
    private TCounterUI tCounter;
<<<<<<< HEAD
    public AudioClip collectionSound; // Reference to the Audio Clip
    
    private bool hasCollected = false;

    public AudioSource audioSource;
    private bool alreadyPlayed = false;


=======
    private bool hasCollected = false;
>>>>>>> parent of 9189c5f... ui and sounds
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
            // 销毁当前的 T
<<<<<<< HEAD
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

=======
>>>>>>> parent of 9189c5f... ui and sounds
            Destroy(gameObject);
        }
    }
}

