using UnityEngine;

public class playMeow : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject.
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if the 'M' key is pressed.
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying) // Ensure it doesn't overlap.
        {
            audioSource.Play();
        }
    }
}
