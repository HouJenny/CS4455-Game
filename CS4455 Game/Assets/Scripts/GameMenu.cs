using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void StartGame()
    {
        if (GameManager.Instance != null) {
            GameManager.Instance.ResetGame();
        }

        SceneManager.LoadScene("TechGreen");

    }
    public void Instructions()
    {   
        
        SceneManager.LoadScene("Instructions");

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void RestartButton() {
        if (GameManager.Instance != null) {
            GameManager.Instance.ResetGame();
        }
        SceneManager.LoadScene("TechGreen");
    }
    public void BackToMainMenu() {

        SceneManager.LoadScene("StartMenu");
    }
}