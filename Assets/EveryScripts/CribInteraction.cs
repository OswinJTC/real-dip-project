using UnityEngine;
using UnityEngine.SceneManagement;

public class CribInteraction : MonoBehaviour
{
    public string mcqSceneName = "MCQ"; // The name of the scene with the MCQ questions
    private bool isPlayerNear = false; // Flag to check if the player is near the crib

    void Update()
    {
        // Check if the player is near the crib and presses the "E" key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player interacts with the crib. Switching to MCQ scene.");
            EnterMCQScene();
        }
    }

    private void EnterMCQScene()
    {
        // Use the TransitionManager to load the MCQ scene with a fade effect, if available
        if (TransitionManager.instance != null)
        {
            TransitionManager.instance.ChangeScene(mcqSceneName);
        }
        else
        {
            Debug.LogWarning("TransitionManager instance not found, loading the scene directly.");
            SceneManager.LoadScene(mcqSceneName); // Fallback to direct scene loading if TransitionManager is not available
        }
    }

    // Detect when the player enters the crib's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the crib.");
        }
    }

    // Detect when the player exits the crib's trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the crib.");
        }
    }
}
