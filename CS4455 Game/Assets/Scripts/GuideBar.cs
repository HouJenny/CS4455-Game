using UnityEngine;
using UnityEngine.UI;

public class GuideBar : MonoBehaviour
{
    public GameObject dialogueBox;  // Use for showing UI element
    private bool isPlayerNearby = false;  // Use for checking if the player is near
    private bool pauseGame = false;

    void Start()
    {
        // Make sure the dialogueBox is hidden at first.
        dialogueBox.SetActive(false);
    }

    // When player enter the trigger of GuideBar
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;  //Player is near
            Debug.Log("Player is near the NPC");
        }
    }

    // When player leave the Guidebar area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;  
            dialogueBox.SetActive(false);  
            Debug.Log("Player left the NPC");
        }
    }

    void Update()
    {
        // If player is near GuideBar and press T
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.T))
        {
            // Visualize dialogueBox
            if (!pauseGame) {
                Time.timeScale = 0f;
            } else {
                Time.timeScale = 1f;
            }
            pauseGame = !pauseGame;
            dialogueBox.SetActive(!dialogueBox.activeSelf);
            Debug.Log("T key pressed, showing dialogue");
        }
    }
}