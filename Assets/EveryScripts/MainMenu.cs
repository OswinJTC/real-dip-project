using UnityEngine;
using UnityEngine.SceneManagement;  // Add this to use SceneManager

public class MainMenu : MonoBehaviour
{
    public MainMenuVideoManager videoManager;  // Reference to the video manager
    public string scene_a = "outsideTerrain";  // Set your new game scene name in the Inspector
    public string scene_b = "outsideTerrain";  // Set your continue scene name in the Inspector

    void Start()
    {
        if (videoManager == null)
        {
            videoManager = FindObjectOfType<MainMenuVideoManager>();
            if (videoManager == null)
            {
                Debug.LogError("MainMenuVideoManager not found!");
            }
        }
    }

    public void OnNewGameButton()
    {
        if (videoManager != null)
        {
            Debug.Log("Playing video for New Game...");
            videoManager.PlayNewGameVideo(scene_a);  // Play the video and load scene_a
        }
    }

    public void OnContinueButton()
    {
        if (videoManager != null)
        {
            Debug.Log("Playing video for Continue...");
            videoManager.PlayNewGameVideo(scene_b);  // Play the video and load scene_b
        }
    }

    public void OnControlsButton()
    {
        Debug.Log("Loading Controls scene...");
        SceneManager.LoadScene(2);  // Load the controls scene directly
    }

    public void OnExitButton()
    {
        Debug.Log("Exiting the application...");
        Application.Quit();  // Exit the game
    }
}
