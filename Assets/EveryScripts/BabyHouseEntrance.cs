using UnityEngine;
using UnityEngine.SceneManagement;

public class BabyHouseEntrance : MonoBehaviour
{
    public Transform player;            // Reference to the player's transform
    public string nextScene = "BBLivingroomScene";  // Scene to load when entering
    private bool isPlayerNear = false; // Flag to check if the player is near the entrance

    void Update()
    {
        // Check if the player is near and presses the "E" key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("E pressed. Attempting to enter the house...");
            EnterHouse();
            UIManager.instance.ShowPrompt("I need to find the fuel...", 5f);
        }
    }

    private void EnterHouse()
    {
        // Check if the player has the key
        if (GameManager.instance != null && GameManager.instance.isKeyActive && GameManager.instance.isKeyInUsed)
        {
            Debug.Log("Player has the key. Entering the house...");
            UIManager.instance.ShowPrompt("Entering the baby house...", 2f);

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
        else
        {
            Debug.Log("Player does not have the key. Cannot enter the house.");
            UIManager.instance.ShowPrompt("Player does not have the key. Cannot enter the house.", 2f);
        }
    }

    private Vector3 GetEntryPosition(string sceneToLoad)
    {
        // Define the entry position for entering the living room scene
        if (sceneToLoad == "BBLivingroomScene")
        {
            return new Vector3(-17.58f, 3.31f, 3.08f);
        }

        // Default fallback position if no specific position is defined
        return new Vector3(0f, 0f, 0f);
    }

    // Detect when the player enters the entrance's trigger collider (3D)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the baby house entrance.");
        }
    }

    // Detect when the player exits the entrance's trigger collider (3D)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the baby house entrance.");
        }
    }
}
