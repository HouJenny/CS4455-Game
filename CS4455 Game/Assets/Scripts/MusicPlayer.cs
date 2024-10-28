using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Singleton instance
    private static MusicPlayer instance;

    private void Awake()
    {
        // Check if there is already an instance of MusicPlayer
        if (instance == null)
        {
            // If not, set this as the instance and don't destroy it between scenes
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy the duplicate
            Destroy(gameObject);
        }
    }
}