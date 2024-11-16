using UnityEngine;
using UnityEngine.SceneManagement;

public class BakeryInteraction1 : MonoBehaviour
{
    public string targetScene = "Bakery Puzzle"; // The scene to load when interacting with the phone
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
            
            // Set clay status to false before switching to the puzzle scene
            GameManager.instance.SetClayStatus(false);
            Debug.Log("Clay status set to false and phone marked active.");
        }

        // Load the target puzzle scene
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
