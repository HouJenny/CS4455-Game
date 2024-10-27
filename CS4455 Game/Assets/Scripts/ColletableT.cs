using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletableT : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            
            // 销毁当前的 T
            Destroy(gameObject);
        }
    }
}
