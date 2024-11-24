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
            cat.transform.localPosition = new Vector3(-.1f, .2f, 0f);
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

        StopCatMovement();

        cat.transform.SetParent(scooter.transform);
        cat.transform.localPosition = new Vector3(-.1f, .2f, 0f);
        cat.transform.localRotation = Quaternion.Euler(0, 90, 0);

        Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();
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

        cat.transform.SetParent(null);

        Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();
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
            0, // Keep Y-axis unchanged
            Random.Range(-teleportRange.y, teleportRange.y)
        );

        // Teleport both scooter and cat
        scooter.transform.position += teleportOffset;

        // Ensure the cat maintains its position relative to the scooter
        if (isOnScooter)
        {
            cat.transform.localPosition = new Vector3(-.1f, .2f, 0f);
            cat.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
