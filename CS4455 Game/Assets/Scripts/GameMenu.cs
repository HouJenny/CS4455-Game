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
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}