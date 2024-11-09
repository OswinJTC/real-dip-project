using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public MainMenuVideoManager videoManager;  // Reference to the video manager
    public Canvas videoCanvas;  // Reference to the Canvas to show during video playback
    public string scene_a = "outsideTerrain";  // New game scene
    public string scene_b = "outsideTerrain";  // Continue scene

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

        // Ensure the canvas is hidden initially
        if (videoCanvas != null)
        {
            videoCanvas.gameObject.SetActive(false);
        }
    }

    public void OnNewGameButton()
    {
        if (videoManager != null)
        {
            Debug.Log("Playing video for New Game...");
            StartCoroutine(DelayedShowCanvas());  // Start the coroutine with delay
            videoManager.PlayNewGameVideo(scene_a);  // Play the video and load scene_a
        }
    }

    public void OnContinueButton()
    {
        if (videoManager != null)
        {
            Debug.Log("Playing video for Continue...");
            StartCoroutine(DelayedShowCanvas());  // Start the coroutine with delay
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

    // Coroutine to show the canvas with a delay
    private IEnumerator DelayedShowCanvas()
    {
        yield return new WaitForSeconds(7);  // Wait for 5 seconds
        if (videoCanvas != null)
        {
            videoCanvas.gameObject.SetActive(true);  // Show the canvas after delay
        }
    }

    // Method to hide the canvas, called when the video ends
    public void HideVideoCanvas()
    {
        if (videoCanvas != null)
        {
            videoCanvas.gameObject.SetActive(false);
        }
    }
}
