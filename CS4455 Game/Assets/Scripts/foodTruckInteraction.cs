using UnityEngine;

public class foodTruckInteraction : MonoBehaviour
{
    public GameObject normalWindow;     
    public GameObject brokenWindow;     
    public AudioClip shatterSound;      
    public AudioSource shatterAudioSource; 
    private bool isBroken = false;     

    private void Start()
    {
        
        normalWindow.SetActive(true);

        
        if (shatterAudioSource == null)
        {
            Debug.LogError("Shatter AudioSource is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBroken && other.CompareTag("Player"))
        {
            BreakWindow();
        }
    }

    private void BreakWindow()
    {
        
        normalWindow.SetActive(false);  
        brokenWindow.SetActive(true);   
        isBroken = true;                
        Debug.Log("Window broken!");    

        
        if (shatterSound != null && shatterAudioSource != null)
        {
            shatterAudioSource.PlayOneShot(shatterSound); 
        }

        Debug.Log("Window broken!");
    }
}