using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BBHouseEntranceVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // The VideoPlayer component
    public GameObject canvas;  // Reference to the canvas you want to hide

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned to BBHouseEntranceVideoManager!");
        }

        if (canvas == null)
        {
            Debug.LogError("Canvas not assigned to BBHouseEntranceVideoManager!");
        }
    }

    // Method to play the video and transition to the basement scene
    public IEnumerator PlayVideoAndChangeScene(string sceneName)
    {
        // Set your video URL or clip (or configure in Inspector)
        videoPlayer.url = "https://drive.google.com/uc?export=download&id=1qFcVGC0lueytq7uEDdZYS9_6x91l4WpR";
        Debug.Log("Video URL set: " + videoPlayer.url);

        // Prepare the video
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null; // Wait until the video is fully prepared
        }

        // Hide the canvas before playing the video
        if (canvas != null)
        {
            canvas.SetActive(false);  // Hide the canvas
        }

        // Play the video
        videoPlayer.Play();
        Debug.Log("Video playing...");

        // Wait until the video finishes
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Re-enable the canvas after the video ends
        if (canvas != null)
        {
            canvas.SetActive(true);  // Show the canvas
        }

        // Transition to the new scene
        Debug.Log("Video finished. Transitioning to " + sceneName);
        SceneManager.LoadScene(sceneName);  // Load the scene after the video
        UIManager.instance.ShowPrompt("I need to find the fuel...", 5f);
    }
}
