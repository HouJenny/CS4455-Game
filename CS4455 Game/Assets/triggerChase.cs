using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerChase : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        // Check if the player is the one entering the trigger
        if (other.CompareTag("Player")) // Ensure your player has the "Player" tag
        {
            // Load the specified scene
            SceneManager.LoadScene("Chase");
        }
    }
}

