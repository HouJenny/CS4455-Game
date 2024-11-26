using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class ScooterInteraction : MonoBehaviour
{
    public GameObject cat;
    public GameObject scooter;
    public ScooterController scooterController;
    public CharacterInputController catController;
    public RootMotionControlScript rootMotionControl; // Reference to RootMotionControlScript

    private Animator catAnimator;
    private bool isNearScooter = false;
    public bool isOnScooter = false;

    // Battery system
    public float maxBattery = 100f;
    public float currentBattery;
    public float batteryDrainRate = 0.2f; // Battery drain per second when scooter is active

    public TextMeshProUGUI batteryText;

    // Teleportation range
    public Vector2 teleportRange = new Vector2(10f, 10f);

    void Start()
    {
        scooterController.enabled = false;
        rootMotionControl.enabled = true;
        catAnimator = cat.GetComponent<Animator>();
        currentBattery = maxBattery;
        batteryText.gameObject.SetActive(true);
        DismountScooter();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == cat)
        {
            isNearScooter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == cat)
        {
            isNearScooter = false;
        }
    }

    void Update()
    {
        if (isOnScooter)
        {
            DrainBattery();
            cat.transform.localPosition = new Vector3(0f, .2f, 0f);
            cat.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isNearScooter && !isOnScooter)
            {
                MountScooter();
            }
            else if (isOnScooter)
            {
                DismountScooter();
            }
        }

        if (isOnScooter && currentBattery <= 0)
        {
            DismountScooter();
        }
    }

    private void MountScooter()
    {
        scooterController.enabled = true;
        rootMotionControl.enabled = false;
        isOnScooter = true;

        catAnimator.SetBool("isOnScooter", isOnScooter);

        StopCatMovement();

        cat.transform.SetParent(scooter.transform);
        cat.transform.localPosition = new Vector3(0f, .2f, 0f);
        cat.transform.localRotation = Quaternion.Euler(0, 90, 0);

        Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Scooter"));

        if (catRigidbody != null)
        {
            catRigidbody.isKinematic = true;
        }
    }

    private void DismountScooter()
    {
        scooterController.enabled = false;
        rootMotionControl.enabled = true;
        isOnScooter = false;

        catAnimator.SetBool("isOnScooter", isOnScooter);

        cat.transform.SetParent(null);

        Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Scooter"), false);

        if (catRigidbody != null)
        {
            catRigidbody.isKinematic = false;
        }
    }

    private void DrainBattery()
    {
        if (currentBattery > 0)
        {
            currentBattery -= batteryDrainRate * Time.deltaTime;
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
            UpdateBatteryDisplay();
        }
    }

    void UpdateBatteryDisplay()
    {
        int displayBattery = Mathf.CeilToInt(currentBattery);
        batteryText.text = displayBattery.ToString() + "%";
    }

    private void StopCatMovement()
    {
        if (catAnimator != null)
        {
            catAnimator.SetFloat("velx", 0);
            catAnimator.SetFloat("vely", 0);
        }

        if (catController != null)
        {
            catController.Forward = 0;
            catController.Turn = 0;
        }
    }

    public void RechargeBattery(float amount)
    {
        currentBattery += amount;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
        UpdateBatteryDisplay();
    }

    public void TeleportCatAndScooter()
    {
        Vector3 teleportOffset = new Vector3(
            Random.Range(-teleportRange.x, teleportRange.x),
            1, // Keep Y-axis unchanged
            Random.Range(-teleportRange.y, teleportRange.y)
        );

		// Reset the scooter's state to ensure it is no longer associated with the cat
        DismountScooter();
    
        // Teleport the cat to a new position, independent of the scooter
        if (cat != null)
        {
            cat.transform.position += teleportOffset;
            cat.transform.SetParent(null); // Detach the cat from the scooter
            cat.transform.localRotation = Quaternion.identity; // Reset rotation if necessary
        }
    
        
    }
}