using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI Canvas
    private bool isPaused = false;

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Escape key pressed"); // For debugging
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f;          // Resume game time
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;          // Pause game time
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }
}


