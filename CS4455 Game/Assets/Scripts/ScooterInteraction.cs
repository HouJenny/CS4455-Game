using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScooterInteraction : MonoBehaviour
{
   public GameObject cat;
    public GameObject scooter;
    public ScooterController scooterController;
    public CatController catController;
    private bool isNearScooter = false;
    private bool isOnScooter = false;

    void Start() {
        scooterController.enabled = false;
        catController.enabled = true;
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject == cat) {
            isNearScooter = true;
        }
    }

    void OnTriggerExit(Collider other) {
        Debug.Log("collision exit");
        if (other.gameObject == cat) {
            isNearScooter = false;
        }
    }

    void Update()
    {
        
        if (isNearScooter && !isOnScooter && Input.GetKeyDown(KeyCode.E))
        {
            scooterController.enabled = true;
            catController.enabled = false;
            isOnScooter = true;
            cat.transform.SetParent(scooter.transform);
            cat.transform.localPosition = new Vector3(-.3f, .7f, 0);
            Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();
            if (catRigidbody != null)
            {
                catRigidbody.isKinematic = true;
            }
        }
        if (isOnScooter && Input.GetKey(KeyCode.Escape)) {
            scooterController.enabled = false;
            catController.enabled = true;
            isOnScooter = false;
            cat.transform.SetParent(null);
            Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();
            if (catRigidbody != null)
            {
                catRigidbody.isKinematic = false;
            }
        }
    }
}
