using UnityEngine;

public class BentoBus : MonoBehaviour
{
    public GameObject potionPrefab; 
    public Transform spawnPoint;    
    private bool playerInRange = false; 
    private bool isInteracted = false;  

    void Update()
    {
        
        if (playerInRange && Input.GetKeyDown(KeyCode.T) && !isInteracted)
        {
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
        isInteracted = true;

        
        if (potionPrefab != null && spawnPoint != null)
        {
            Instantiate(potionPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}