using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    public float speed = 5f;              // Speed of movement
    public float turningSpeed = 5f;       // Speed of turning
    public Camera mainCamera;             // Reference to the main camera
    private Rigidbody rb;                 // Reference to the Rigidbody component

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        // Automatically assign the Main Camera if not manually assigned in the Inspector
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void FixedUpdate()
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

        if (moveDirection != Vector3.zero)
        {
            // Apply force to move the player in the calculated direction
            rb.AddForce(moveDirection * speed * 50f); // Adjust the multiplier as needed for smooth movement
        }
        else
        {
            // Stop the player immediately when no input is detected
            rb.linearVelocity = Vector3.zero;  // Stop any movement
        }

        // Clamp the player's velocity to ensure it does not exceed the desired speed
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, speed);
    }
}
