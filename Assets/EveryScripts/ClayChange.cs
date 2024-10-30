using UnityEngine;
using UnityEngine.SceneManagement;

public class ClayChange : MonoBehaviour
{
    private string previousRealScene = ""; // Store the name of the real scene before switching to clay
    private Vector3 storedPlayerPosition; // Store the player's position when switching from real to clay

    void Update()
    {
        // Toggle between real and clay scenes with the Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleClayStatus();
        }
    }

    private void ToggleClayStatus()
    {
        // Get the current scene name from the GameManager
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check the current status from GameManager to decide the transition
        if (GameManager.instance.GetClayStatus())
        {
            // Switch back to the real scene if currently in clay
            if (!string.IsNullOrEmpty(previousRealScene))
            {
                GameManager.instance.SetPlayerEntryPosition(storedPlayerPosition);
                SceneManager.LoadScene(previousRealScene);
                GameManager.instance.SetClayStatus(false);
                Debug.Log("Switched back to the real scene: " + previousRealScene);
            }
        }
        else
        {
            // Store the player's current position before switching to clay
            storedPlayerPosition = GameManager.instance.player.transform.position;
            Debug.Log($"Stored player position before switching to clay: {storedPlayerPosition}");

            // Determine the corresponding clay scene name based on the current scene
            string claySceneName = GetCorrespondingClayScene(currentSceneName);

            if (!string.IsNullOrEmpty(claySceneName))
            {
                previousRealScene = currentSceneName;
                GameManager.instance.SetPlayerEntryPosition(storedPlayerPosition);
                SceneManager.LoadScene(claySceneName);
                GameManager.instance.SetClayStatus(true);
                Debug.Log("Switched to the clay scene: " + claySceneName);
            }
        }
    }

    private string GetCorrespondingClayScene(string currentScene)
    {
        // Map each real scene to its corresponding clay scene
        switch (currentScene)
        {
            case "BedroomScene": return "BBBedroomClay";
            case "BBLivingroomScene": return "BBLRoomClay";
            case "KitchenScene": return "BBKitchenClay";
            // Add more mappings as needed
            default: return "";
        }
    }
}
