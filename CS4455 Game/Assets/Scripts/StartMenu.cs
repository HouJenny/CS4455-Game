using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuUI;
    public GameObject pauseMenuUI; // Reference to the Pause Menu to make sure it's off initially

    private void Start()
    {
        startMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false); // Ensure pause menu is off at start
        Time.timeScale = 0f;  // Pauses the game until Start Game is clicked
    }

    public void StartGame()
    {
        startMenuUI.SetActive(false);
        Time.timeScale = 1f;  // Resumes the game
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


