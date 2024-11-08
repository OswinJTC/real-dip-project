using UnityEngine;
using UnityEngine.SceneManagement;

public class PaperInteraction1 : MonoBehaviour
{
    public string targetScene = "Paper Puzzle"; // The scene to load when interacting with the paper
    private bool isPlayerNear = false; // Flag to check if the player is near the paper

    void Update()
    {
        // Check if the player is near the paper and presses the 'E' key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithPaper();
        }
    }

    private void InteractWithPaper()
    {
        Debug.Log("Interacting with the paper. Saving player position and loading the paper Puzzle scene...");

        // Save the player's current position in GameManager before loading the puzzle scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && GameManager.instance != null)
        {
            GameManager.instance.savedPlayerPosition = player.transform.position;
        }

        // Load the target scene
        SceneManager.LoadScene(targetScene);
    }

    // Detect when the player enters the paper's trigger collider (3D)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the paper.");
        }
    }

    // Detect when the player exits the paper's trigger collider (3D)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the paper.");
        }
    }
}
