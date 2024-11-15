using UnityEngine;
using TMPro; // Import TextMeshPro namespace.

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
            DontDestroyOnLoad(gameObject); // Keep this object between scenes.
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
        collectiblesText.text = $"T's Stolen: {collectibleCount}" +
            $"         Heists Remaining: {heistCount}";
    }
}

