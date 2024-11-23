using UnityEngine;

public class BentoBus : MonoBehaviour
{
    public GameObject potionPrefab; 
    public Transform spawnPoint;    
    private bool playerInRange = false; 
    private float cooldown = 15.0f;
    private float currTime;
    private float lastInteraction;

    void Awake() {
        currTime = Time.realtimeSinceStartup;
        lastInteraction = Time.realtimeSinceStartup;
    }

    void Update()
    {   
        lastInteraction = Time.realtimeSinceStartup;
        if (playerInRange && Input.GetKeyDown(KeyCode.T) && currTime <= lastInteraction)
        {
            Debug.Log("last interaction" + lastInteraction);
            Debug.Log("next available" + currTime);
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerInRange = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerInRange = false; 
        }
    }

    private void Interact()
    {
        lastInteraction = Time.realtimeSinceStartup;
        Debug.Log("last interaction" + lastInteraction);
        currTime = Time.realtimeSinceStartup + cooldown;
        Debug.Log("next available" + currTime);
        
        if (potionPrefab != null && spawnPoint != null)
        {
            Instantiate(potionPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}