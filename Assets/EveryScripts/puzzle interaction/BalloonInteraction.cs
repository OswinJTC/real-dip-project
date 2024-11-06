using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonInteraction : MonoBehaviour
{
    public string targetScene = "Balloon Puzzle"; // The scene to load when interacting with the balloon
    private bool isPlayerNear = false; // Flag to check if the player is near the balloon

    void Update()
    {
        // Check if the player is near the balloon and presses the 'E' key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithBalloon();
        }
    }

    private void InteractWithBalloon()
    {
        Debug.Log("Interacting with the balloon. Saving player position and loading the Balloon Puzzle scene...");

        // Save the player's current position in GameManager before loading the puzzle scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && GameManager.instance != null)
        {
            GameManager.instance.savedPlayerPosition = player.transform.position;
        }
        
        // Load the target scene (Balloon Puzzle)
        SceneManager.LoadScene(targetScene);
    }

    // Detect when the player enters the balloon's trigger collider (2D)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the balloon.");
        }
    }

    // Detect when the player exits the balloon's trigger collider (2D)
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the balloon.");
        }
    }
}
