using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    public Transform player;      // Reference to the player's transform
    public float distance = 5.0f; // Distance behind the player
    public float height = 2.0f;   // Height above the player
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement

    private Vector3 offset;

    void Start()
    {
        // Set the initial offset based on the distance and height
        offset = new Vector3(0, height, -distance);

        // Set the camera's initial position instantly behind the player
        Vector3 initialPosition = player.position + player.rotation * offset;
        transform.position = initialPosition;

        // Ensure the camera is looking at the player from the start
        transform.LookAt(player);
    }

    void LateUpdate()
    {
        // Desired position for the camera (behind and above the player)
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // Smooth the camera's transition to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Keep the camera looking at the player
        transform.LookAt(player);
    }
}
