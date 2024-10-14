using UnityEngine;
using UnityEngine.SceneManagement;

public class HourglassInteraction : MonoBehaviour
{
    public float detectionRadius = 3f;  // The radius within which the player can interact with the hourglass
    public Transform player;            // Reference to the player's transform

    private bool isPickedUp = false;    // To ensure hourglass is only picked up once

    void Update()
    {
        // Check distance between player and hourglass
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within range and presses "E" to pick up the hourglass
        if (distanceToPlayer <= detectionRadius && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            PickUpHourglass();
        }
    }

    // Function to handle picking up the hourglass
    void PickUpHourglass()
    {
        // Add the hourglass to the global inventory
        GameManager.instance.AddToInventory("Hourglass");

        // Make the hourglass UI icon visible in the inventory corner through the GameManager's persistent Canvas
        if (GameManager.instance.hourglassUIIcon != null)
        {
            GameManager.instance.hourglassUIIcon.SetActive(true);  // Show the icon
        }

        // Mark the hourglass as picked up
        isPickedUp = true;

        // Optionally, hide or destroy the hourglass in the scene
        gameObject.SetActive(false);

        // Transition to the next scene after picking up
        SceneManager.LoadScene("KemasEnvironmentShaderProblem");
    }
}
