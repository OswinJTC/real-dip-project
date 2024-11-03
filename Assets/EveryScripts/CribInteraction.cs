using UnityEngine;

public class CribInteraction : MonoBehaviour
{
    public GameObject panel; // Reference to the panel GameObject
    private bool isPlayerNear = false; // Flag to check if the player is near the crib

    void Start()
    {
        // Make sure the panel starts as inactive
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the player is near the crib and presses the "E" key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            TogglePanel(); // Call method to toggle panel visibility
        }
    }

    private void TogglePanel()
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf; // Check current state
            panel.SetActive(!isActive); // Toggle active state
            Debug.Log(isActive ? "Panel closed." : "Panel opened."); // Log current action
        }
        else
        {
            Debug.LogError("Panel is not assigned in the Inspector!");
        }
    }

    // Detect when the player enters the crib's trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the crib.");
        }
    }

    // Detect when the player exits the crib's trigger collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the crib.");

            // Optionally close the panel when player exits
            if (panel != null && panel.activeSelf)
            {
                panel.SetActive(false); // Close the panel when player leaves
                Debug.Log("Panel closed due to player exit.");
            }
        }
    }
}