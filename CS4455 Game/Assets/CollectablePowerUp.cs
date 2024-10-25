using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePowerUp : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
