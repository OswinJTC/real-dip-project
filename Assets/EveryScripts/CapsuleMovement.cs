using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    public float speed = 5f;              // Speed of movement
    public float turningSpeed = 5f;       // Speed of turning
    public Camera mainCamera;              // Reference to the main camera

    void Update()
    {
        // Get input from the vertical axis (W/S or Up/Down arrow keys)
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the forward direction relative to the camera
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0; // Ignore the y component to prevent upward movement
        forward.Normalize(); // Normalize to ensure consistent speed

        // Calculate the right direction relative to the camera
        Vector3 right = mainCamera.transform.right;
        right.y = 0; // Ignore the y component to prevent upward movement
        right.Normalize(); // Normalize to ensure consistent speed

        // Calculate the movement direction based on input
        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

        // Move the capsule in the calculated direction
        transform.position += moveDirection * speed * Time.deltaTime;

        // Rotate capsule to face the direction of movement
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
        }
    }
}
