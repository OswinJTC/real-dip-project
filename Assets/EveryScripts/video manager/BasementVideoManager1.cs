using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BasementVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // The VideoPlayer component
    private GameObject persistentCanvas;  // Reference to the PersistentCanvas

    void Start()
    {
        // Check if VideoPlayer is assigned
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned to BasementVideoManager!");
        }

        // Find the PersistentCanvas through GameManager
        AllocatePersistentCanvas();
    }

    private void AllocatePersistentCanvas()
    {
        // Access the PersistentCanvas from GameManager
        if (GameManager.instance != null && GameManager.instance.PersistentCanvas != null)
        {
            persistentCanvas = GameManager.instance.PersistentCanvas.gameObject;
            Debug.Log("PersistentCanvas found successfully via GameManager.");
        }
        else
        {
            Debug.LogError("PersistentCanvas not found! Ensure it is assigned in the GameManager.");
        }
    }

    // Method to play the video and transition to the basement scene
    public IEnumerator PlayVideoAndChangeScene(string sceneName)
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer is not assigned!");
            yield break;
        }

        // Set your video URL or clip
        videoPlayer.url = "https://drive.google.com/uc?export=download&id=18q10pUimN5ozUCthGYxucWTHmmK-C33t";
        Debug.Log("Video URL set: " + videoPlayer.url);

        // Prepare the video
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null; // Wait until the video is fully prepared
        }

        // Hide the PersistentCanvas before playing the video
        if (persistentCanvas != null)
        {
            persistentCanvas.SetActive(false);  // Hide the canvas
            Debug.Log("PersistentCanvas hidden.");
        }
        else
        {
            Debug.LogWarning("PersistentCanvas is null. Cannot hide.");
        }

        // Play the video
        videoPlayer.Play();
        Debug.Log("Video playing...");

        // Wait until the video finishes
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Re-enable the PersistentCanvas after the video ends
        if (persistentCanvas != null)
        {
            persistentCanvas.SetActive(true);  // Show the canvas
            Debug.Log("PersistentCanvas shown.");
        }
        else
        {
            Debug.LogWarning("PersistentCanvas is null. Cannot show.");
        }

        // Transition to the new scene
        Debug.Log("Video finished. Transitioning to " + sceneName);
        SceneManager.LoadScene(sceneName);  // Load the scene after the video
        UIManager.instance.ShowPrompt("Look around for help..", 8f);
    }
}
