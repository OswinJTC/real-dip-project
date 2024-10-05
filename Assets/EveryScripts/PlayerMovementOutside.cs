using UnityEngine;

public class PlayerMovementOutside : MonoBehaviour
{
    public float speed = 3f;             // Speed of movement
    public float rotationSpeed = 8f;   // Speed of turning
    private Rigidbody rb;                // Reference to the Rigidbody component

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input from the vertical axis (W/S or Up/Down arrow keys)
        float verticalInput = Input.GetAxis("Vertical");   // Forward and backward movement
        float horizontalInput = Input.GetAxis("Horizontal"); // Left and right rotation

        // Move the player forward or backward
        if (verticalInput != 0)
        {
            Vector3 moveDirection = transform.forward * verticalInput;  // Move based on player's current facing direction
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        }

        // Rotate the player left or right
        if (horizontalInput != 0)
        {
            float rotation = horizontalInput * rotationSpeed * Time.deltaTime;  // Rotate the player around the Y axis
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));
        }
    }
}
