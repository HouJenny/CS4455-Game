using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Drag the player object here in the Inspector
    public Vector3 offset;   // Adjust this offset to set the camera's distance from the player
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optional: Make the camera look at the player
        transform.LookAt(player);
    }
}
