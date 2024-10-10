using UnityEngine;
using UnityEngine.SceneManagement;

public class Game1SuitcaseInteraction : MonoBehaviour
{
    public float interactionRadius = 1f; // Radius for interaction
    private bool isPlayerNear = false; // To check if player is near the suitcase
    private GameObject player; // Reference to the player

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Debug.Log("Player distance from suitcase: " + distance);

            // Check if player is near and the "E" key is pressed
            if (isPlayerNear && distance <= interactionRadius && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player is within interaction radius of the suitcase and pressed E.");
                StartPuzzleGame();
            }
        }
    }

    private void StartPuzzleGame()
    {
        // Load the puzzle game scene (replace "PuzzleGame" with your actual puzzle scene name)
        Debug.Log("Loading scene: PuzzleGame");
        SceneManager.LoadScene("Bakery");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (make sure the player's tag is "Player")
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isPlayerNear = true;
            Debug.Log("Player entered the suitcase trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset when the player leaves the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null;
            Debug.Log("Player exited the suitcase trigger zone.");
        }
    }
}
