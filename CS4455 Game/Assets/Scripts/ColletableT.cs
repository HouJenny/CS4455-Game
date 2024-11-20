using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletableT : MonoBehaviour
{
    private TCounterUI tCounter;
    private bool hasCollected = false;
    void Start()
    {
        // Find the TIconCounterUI script in the scene
        tCounter = FindObjectOfType<TCounterUI>();
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player") && !hasCollected)
        {
            hasCollected = true;
            GameManager.Instance.UpdateCollectiblesUI();
            Debug.Log("hit T");
            // 销毁当前的 T
            Destroy(gameObject);
        }
    }
}
