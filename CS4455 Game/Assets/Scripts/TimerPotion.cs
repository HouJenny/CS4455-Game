using UnityEngine;

public class TimerPotion : MonoBehaviour
{
    public float timeToAdd = 30f; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            GameTimer gameTimer = FindObjectOfType<GameTimer>();
            if (gameTimer != null)
            {
                gameTimer.AddTime(timeToAdd); 
            }

            Destroy(gameObject); 
        }
    }
}
