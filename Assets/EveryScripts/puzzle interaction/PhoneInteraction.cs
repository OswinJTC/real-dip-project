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
        Debug.Log("Interacting with the phone. Saving positions and loading the Phone Puzzle scene...");

        // Save the player's and monster's positions and set isPhoneActive to true in GameManager
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

            // Set the phone's active status in the GameManager
            GameManager.instance.isPhoneActive = true;
            GameManager.instance.UpdateInventoryUI(); // Update the inventory UI to reflect the change
            UIManager.instance.ShowPrompt("Phone collected...all the best for the puzzle...", 2f);
            
            // Set clay status to false before switching to the puzzle scene
            GameManager.instance.SetClayStatus(false);
            Debug.Log("Clay status set to false and phone marked active.");
        }

        // Load the target puzzle scene
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
