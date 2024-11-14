using UnityEngine;
using UnityEngine.SceneManagement;

public class ClayChange : MonoBehaviour
{
    public string previousRealScene = ""; // Store the name of the real scene before switching to clay

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
        // Check if the player has the hourglass before allowing clay/real switch
        if (!GameManager.instance.isHourglassActive)
        {
            Debug.Log("Hourglass is not active. Cannot switch between clay and real scenes.");
            UIManager.instance.ShowPrompt("Hourglass is not active. Cannot switch between clay and real scenes.", 2f);
            return; // Exit the function if the hourglass is not active
        }

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (GameManager.instance.GetClayStatus())
        {
            if (!string.IsNullOrEmpty(previousRealScene))
            {
                // Save the monster's current position before switching back to the real scene
                GameManager.instance.SaveMonsterPosition(currentSceneName);
                GameManager.instance.SetClayStatus(false);
                TransitionManager.instance.ChangeScene(previousRealScene);
                //SceneManager.LoadScene(previousRealScene);
                Debug.Log("Switched back to the real scene: " + previousRealScene);
            }
        }
        else
        {
            // Save both player and monster positions before switching to clay
            GameManager.instance.SavePlayerPosition(currentSceneName);
            GameManager.instance.SaveMonsterPosition(currentSceneName);

            string claySceneName = GetCorrespondingClayScene(currentSceneName);
            if (!string.IsNullOrEmpty(claySceneName))
            {
                previousRealScene = currentSceneName;
                GameManager.instance.SetClayStatus(true);
                TransitionManager.instance.ChangeScene(claySceneName);
                //SceneManager.LoadScene(claySceneName);
                Debug.Log("Switched to the clay scene: " + claySceneName);
            }
        }
    }

    private string GetCorrespondingClayScene(string currentScene)
    {
        // Map each real scene to its corresponding clay scene
        switch (currentScene)
        {
            case "BedroomScene":
                return "BBBedroomClay";
            case "BBLivingroomScene":
                return "BBLRoomClay";
            case "KitchenScene":
                return "BBKitchenClay";
            // Add more mappings as needed
            default:
                return "";
        }
    }
}
