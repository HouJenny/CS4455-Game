using UnityEngine;

public class TimerPotion : MonoBehaviour
{
    public float timeToAdd = 30f; 

    private bool hasCollected = false;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !hasCollected)
        {
            
            GameTimer gameTimer = FindObjectOfType<GameTimer>();
            if (gameTimer != null)
            {
                gameTimer.AddTime(timeToAdd); 
            }
            hasCollected = true;
        }
    }
}
