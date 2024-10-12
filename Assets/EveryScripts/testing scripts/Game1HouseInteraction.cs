using UnityEngine;
using UnityEngine.SceneManagement;

public class Game1HouseInteraction : MonoBehaviour
{
    public float interactionRadius = 3f; // Radius for interaction
    private bool isPlayerNear = false; // To check if player is near
    private GameObject player; // Reference to the player

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Debug.Log("Player distance from house: " + distance);

            // Check if player is near and the "E" key is presseds
            if (isPlayerNear && distance <= interactionRadius && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player is within interaction radius and pressed E.");
                EnterHouse();
            }
        }
    }

    private void EnterHouse()
    {
        // Load the interior scene (replace "InteriorScene" with your actual scene name)
        Debug.Log("Loading scene: Warehouse");
        SceneManager.LoadScene("Warehouse");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (make sure the player's tag is "Player")
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isPlayerNear = true;
            Debug.Log("Player entered the trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset when the player leaves the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null;
            Debug.Log("Player exited the trigger zone.");
        }
    }
}
