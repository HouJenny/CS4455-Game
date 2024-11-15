using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPowerUp : MonoBehaviour
{
    public float batteryRechargeAmount = 2f; // Amount of battery to recharge

    void OnTriggerEnter(Collider other)
    {
        // Ensure the colliding object has the correct tag
        ScooterInteraction scooterInteraction = other.GetComponentInParent<ScooterInteraction>();
        if (scooterInteraction != null)
        {
            Debug.Log("ScooterInteraction found, recharging battery...");
            scooterInteraction.RechargeBattery(batteryRechargeAmount);

            // Destroy 
            Destroy(gameObject);
        }
    }
}

