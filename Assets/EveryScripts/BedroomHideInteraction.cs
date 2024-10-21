using UnityEngine;
using UnityEngine.SceneManagement;

public class BedroomHideInteraction : MonoBehaviour
{
    public string hideSceneName = "BBHideBRoom"; // The name of the scene to switch to for hiding
    private bool isPlayerNear = false; // Flag to check if the player is near the cupboard
    private string previousSceneName; // Store the previous scene name

    void Update()
    {
        // Check if the player is near the cupboard and presses the "E" key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == hideSceneName)
            {
                // If the player is already in the hiding scene, go back to the previous scene
                ReturnToPreviousScene();
            }
            else
            {
                // Otherwise, enter the hiding scene
                EnterHideScene();
            }
        }

        if (SceneManager.GetActiveScene().name == hideSceneName && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Leave the the cupboard.");
            ReturnToPreviousScene();
        
        }
    }

    private void EnterHideScene()
    {
        // Store the current scene name as the previous scene before switching
        previousSceneName = SceneManager.GetActiveScene().name;

        // Use the TransitionManager to load the hide scene with a fade effect, if available
        if (TransitionManager.instance != null)
        {
            TransitionManager.instance.ChangeScene(hideSceneName);
        }
        else
        {
            Debug.LogWarning("TransitionManager instance not found, loading the scene directly.");
            SceneManager.LoadScene(hideSceneName); // Fallback to direct scene loading if TransitionManager is not available
        }
    }

    private void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            // Use the TransitionManager to return to the previous scene with a fade effect, if available
            if (TransitionManager.instance != null)
            {
                TransitionManager.instance.ChangeScene(previousSceneName);
            }
            else
            {
                Debug.LogWarning("TransitionManager instance not found, loading the previous scene directly.");
                SceneManager.LoadScene(previousSceneName); // Fallback to direct scene loading if TransitionManager is not available
            }
        }
        else
        {
            Debug.LogWarning("Previous scene name is not set. Cannot return to the previous scene.");
        }
    }

    // Detect when the player enters the cupboard's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the cupboard.");
        }
    }

    // Detect when the player exits the cupboard's trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the cupboard.");
        }
    }
}
