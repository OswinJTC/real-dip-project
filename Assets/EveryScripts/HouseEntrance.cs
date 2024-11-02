using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEntrance : MonoBehaviour
{
    public Transform player;            // Reference to the player's transform
    public string nextScene = "TutLRoomDScene";  // Scene to load when entering

    void Update()
    {
        // Check if the player is near and presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            Debug.Log("E pressed. Player entering the house...");
            EnterHouse();
        }
    }

    private bool isPlayerNear = false; // Flag to check if the player is near the entrance

    private void EnterHouse()
    {
        Debug.Log("Entering the house...");

        // Set the player's entry position when entering the house
        Vector3 entryPosition = GetEntryPosition(nextScene);
        GameManager.instance.SetPlayerEntryPosition(entryPosition);

        // Use the TransitionManager to change scenes
        if (TransitionManager.instance != null)
        {
            TransitionManager.instance.ChangeScene(nextScene);
        }
        else
        {
            Debug.LogWarning("TransitionManager instance not found, loading the scene directly.");
            SceneManager.LoadScene(nextScene);
        }
    }

    private Vector3 GetEntryPosition(string sceneToLoad)
    {
        // Define the entry position for entering the living room scene
        if (sceneToLoad == "TutLRoomDScene")
        {
            return new Vector3(5.23f, 5.17f, 11.34f);
        }

        // Default fallback position if no specific position is defined
        return new Vector3(0f, 0f, 0f);
    }

    // Detect when the player enters the entrance's trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the entrance.");
        }
    }

    // Detect when the player exits the entrance's trigger collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the entrance.");
        }
    }
}
