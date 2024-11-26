using UnityEngine;
using TMPro; // Import TextMeshPro namespace.
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TMP_Text collectiblesText; // Reference to the UI text element.
    private int collectibleCount = 0; // Track how many collectibles the player has.
    private int heistCount = 5;

    private void Awake()
    {
        // Ensure only one GameManager instance exists (Singleton pattern).
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object between scenes.
        }
        else
        {
            Destroy(gameObject); // Avoid duplicates.
        }
    }

    // Update the UI text element with the latest count.
    public void UpdateCollectiblesUI()
    {
        heistCount--; 
        collectibleCount++;
        Debug.Log(heistCount);
        collectiblesText.text = $"{collectibleCount}" +
            $"             {heistCount}";
        
    }

    public void Update() {
        if (heistCount == 0) {
            LoadEndMenu();
        }
    }

    public void ResetGame() {
        collectibleCount = 0;
        heistCount = 5;
        collectiblesText.text = $"{collectibleCount}" +
            $"             {heistCount}";
        Destroy(gameObject);
    }

    public void LoadEndMenu()
    {
        StartCoroutine(LoadWinScreenWithDelay());
    }

    private IEnumerator LoadWinScreenWithDelay()
    {
        
        yield return new WaitForSeconds(.5f); // Wait for the delay duration
        // Load the win screen scene
        SceneManager.LoadScene("EndMenu");
    }

}

