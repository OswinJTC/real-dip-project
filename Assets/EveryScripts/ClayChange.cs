using UnityEngine;
using UnityEngine.SceneManagement;

public class ClayChange : MonoBehaviour
{
    private string previousRealScene = ""; // Store the name of the real scene before switching to clay

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
        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check the current status from GameManager to decide the transition
        if (GameManager.instance.GetClayStatus())
        {
            // Switch back to the real scene if currently in clay
            if (!string.IsNullOrEmpty(previousRealScene))
            {
                // Restore player and monster positions
                GameManager.instance.SetClayStatus(false);
                SceneManager.LoadScene(previousRealScene);
                Debug.Log("Switched back to the real scene: " + previousRealScene);
            }
        }
        else
        {
            // Store the player's and monster's current positions before switching to clay
            GameManager.instance.SavePlayerPosition(currentSceneName);
            GameManager.instance.SaveMonsterPosition(currentSceneName);

            Debug.Log("Stored player and monster positions before switching to clay.");

            // Determine the corresponding clay scene name based on the current scene
            string claySceneName = GetCorrespondingClayScene(currentSceneName);

            if (!string.IsNullOrEmpty(claySceneName))
            {
                previousRealScene = currentSceneName;
                GameManager.instance.SetClayStatus(true);
                // Do not deactivate the monster when switching to clay
                SceneManager.LoadScene(claySceneName);
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
