using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletablePowerUp : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
