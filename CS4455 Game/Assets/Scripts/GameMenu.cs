using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TechGreen");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}