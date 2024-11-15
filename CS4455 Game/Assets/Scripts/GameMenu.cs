using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void StartGame()
    {
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
}