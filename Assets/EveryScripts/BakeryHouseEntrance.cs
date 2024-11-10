using UnityEngine;
using UnityEngine.SceneManagement;

public class BakeryHouseEntrance : MonoBehaviour
{
    public Transform player;            // Reference to the player's transform
    public string nextScene = "BakeryScene";  // Scene to load when entering

    private bool isPlayerNear = false; // Flag to check if the player is near the entrance

    void Update()
    {
        // Check if the player is near and presses the "E" key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed. Player entering the house...");
            UIManager.instance.ShowPrompt("Player entering the bakery...", 2f);
            EnterHouse();
        }
    }

    private void EnterHouse()
    {
        Debug.Log("Entering the house...");

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

  
  
  
  
  
  
  
  