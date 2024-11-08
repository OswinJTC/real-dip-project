using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneInteraction : MonoBehaviour
{
    public string targetScene = "Phone Puzzle"; // The scene to load when interacting with the phone
    private bool isPlayerNear = false; // Flag to check if the player is near the phone

    void Update()
    {
        // Check if the player is near the phone and presses the 'E' key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithPhone();
        }
    }

    private void InteractWithPhone()
    {
        Debug.Log("Interacting with the phone. Saving player position and loading the Phone Puzzle scene...");

        // Save the player's current position in GameManager before loading the puzzle scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && GameManager.instance != null)
        {
            GameManager.instance.savedPlayerPosition = player.transform.position;
        }

        // Load the target scene
        SceneManager.LoadScene(targetScene);
    }

    // Detect when the player enters the phone's trigger collider (3D)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the phone.");
        }
    }

    // Detect when the player exits the phone's trigger collider (3D)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the phone.");
        }
    }
}