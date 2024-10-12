using UnityEngine;

public class Camera2D5DFollow : MonoBehaviour
{
    public Transform target; // Reference to the player
    public float distance = 10.0f; // Fixed distance from the player
    public float height = 5.0f; // Fixed height of the camera

    void LateUpdate()
    {
        if (target != null)
        {
            // Set the camera's position based on the player's position, but keep it at a fixed distance and height
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y + height, target.position.z - distance);
            
            // Move the camera to the desired position
            transform.position = desiredPosition;

            // Keep the camera's rotation fixed (facing straight in 2.5D)
            transform.rotation = Quaternion.Euler(30f, 0f, 0f); // Adjust the rotation as needed for your 2.5D angle
        }
    }
}
