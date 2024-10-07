using UnityEngine;
using UnityEngine.SceneManagement;  // For scene loading

public class HouseEntrance : MonoBehaviour
{
    private bool isPlayerNear = false;  // Flag to track if the player is near the house

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;  // Set the flag when the player is near
            Debug.Log("Player is near the house. Press E to enter.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset the flag when the player leaves the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    void Update()
    {
        // Check if the player is near the house and presses the "E" key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            EnterHouse();
        }
    }

    void EnterHouse()
    {
        Debug.Log("Entering the house...");
        SceneManager.LoadScene("TutLRoomCScene");  // Load the HAHAHA scene
    }
}
