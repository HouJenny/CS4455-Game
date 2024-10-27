using UnityEngine;

public class TManager : MonoBehaviour
{
    public static TManager instance;
    public Vector3[] tPositions;  // Store every T positions
    public GameObject[] tObjects; // Store every T Objects
    public int currentTIndex = 0; // Current T index

    void Awake()
    {
        // Make sure instance model
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        tObjects = GameObject.FindGameObjectsWithTag("T");

        // Initial tPositions
        tPositions = new Vector3[tObjects.Length];

        // Get every T's Position
        for (int i = 0; i < tObjects.Length; i++)
        {
            tPositions[i] = tObjects[i].transform.position;  
            Debug.Log("T " + i + " postion: " + tPositions[i]);
        }
    }

    // get current T position
    public Vector3 GetCurrentTPosition()
    {
        if (currentTIndex < tPositions.Length)
        {
            return tPositions[currentTIndex];
        }
        else
        {
            Debug.Log("Every T have been collected");
            return Vector3.zero;
        }
    }

    // Player collect current T and will tell the player what is the next postion
    public void CollectT()
    {
        // Destroy current T
        if (currentTIndex < tObjects.Length)
        {
            Destroy(tObjects[currentTIndex]);
        }

        
        if (currentTIndex < tPositions.Length - 1)
        {
            currentTIndex++;  
            Vector3 nextTPosition = GetCurrentTPosition();  
            Debug.Log("Next T's Position " + nextTPosition);  
        }
        else
        {
            Debug.Log("Every T has been collected");
        }
    }
}