using UnityEngine;
using UnityEngine.SceneManagement;

public class BakeryInteraction1 : MonoBehaviour
{
    public string targetScene = "Bakery"; // The scene to load when interacting with the phone
    private bool isPlayerNear = false; // Flag to check if the player is near the phone

    void Update()
    {
        // Check if the player is near the phone and presses the 'E' key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithBakery();
        }
    }

    private void InteractWithBakery()
    {
        Debug.Log("Interacting with the Bakery. Saving player position and loading the Bakery Puzzle scene...");

        // Save the player's current position in GameManager before loading the puzzle scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && GameManager.instance != null)
        {
            GameManager.instance.savedPlayerPosition = player.transform.position;
        }

        // Load the target scene
        SceneManager.LoadScene(targetScene);
    }

    // Detect when the player enters the Bakery's trigger collider (3D)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the Bakery.");
        }
    }

    // Detect when the player exits the Bakery's trigger collider (3D)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the Bakery.");
        }
    }
}
