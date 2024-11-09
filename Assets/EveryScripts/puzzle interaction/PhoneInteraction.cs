using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneInteraction: MonoBehaviour
{
    public string targetScene = "Phone Puzzle"; // The scene to load when interacting with the Phone
    private bool isPlayerNear = false; // Flag to check if the player is near the Phone

    void Update()
    {
        // Check if the player is near the Phone and presses the 'E' key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithPhone();
        }
    }

    private void InteractWithPhone()
    {
        Debug.Log("Interacting with the Phone. Saving positions and loading the Phone Puzzle scene...");

        // Save the player's and monster's positions in GameManager before loading the puzzle scene
        if (GameManager.instance != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject monster = GameObject.FindGameObjectWithTag("Monster");

            if (player != null)
            {
                GameManager.instance.savedPlayerPosition = player.transform.position;
            }

            if (monster != null)
            {
                GameManager.instance.SaveMonsterPosition(SceneManager.GetActiveScene().name);
                
            }

            // Set clay status to false before switching to the puzzle scene
            GameManager.instance.SetClayStatus(false);
            Debug.Log("Clay status set to false.");
        }

        // Load the target puzzle scene
        SceneManager.LoadScene(targetScene);
    }

    // Detect when the player enters the Phone's trigger collider (3D)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the Phone.");
        }
    }

    // Detect when the player exits the Phone's trigger collider (3D)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the Phone.");
        }
    }
}
