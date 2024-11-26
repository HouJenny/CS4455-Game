using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 240f; 
    private float timer;
    public TextMeshProUGUI timerText; 
    public GameObject gameOverPanel;
    public TextMeshProUGUI batteryText;
    public TextMeshProUGUI counter;

    private bool isGameOver = false;

    void Start()
    {
        
        timer = timeLimit;
        isGameOver = false;
        batteryText.gameObject.SetActive(true);
        batteryText.enabled = true;
        counter.enabled = true;
        timerText.gameObject.SetActive(true); 
        Time.timeScale = 1; 
    }

    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime; 
            UpdateTimerDisplay();

            if (timer <= 0)
            {
                GameOver();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        
        int displayTime = Mathf.CeilToInt(timer);
        timerText.text = displayTime.ToString();
    }

    void GameOver()
    {
        isGameOver = true;
        timer = 0; 
        gameOverPanel.SetActive(true); // Show "GameOver" Panel
        batteryText.enabled = false;
        counter.enabled = false;

        timerText.text = "";
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1; 

        if (GameManager.Instance != null) {
            GameManager.Instance.ResetGame();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    
    public void GoToMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("StartMenu"); 
    }
    public void AddTime(float timeToAdd)
    {
        timer += timeToAdd;
    }
    public void DecreaseTime(float amount)
    {
        timer = Mathf.Max(0, timer - amount / 2); 
    }
}