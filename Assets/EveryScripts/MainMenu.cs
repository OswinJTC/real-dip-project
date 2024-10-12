using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Reference to the VideoPlayer component
    public GameObject canvas;        // Reference to the Canvas GameObject (for hiding UI)

    public string scene_a = "KemasEnvironmentShaderProblem";  // Set your new game scene name in the Inspector
    public string scene_b = "KemasEnvironmentShaderProblem"; // Set your continue scene name in the Inspector

    public void OnNewGameButton()
    {
        StartCoroutine(PlayVideoAndLoadScene(scene_a));  // Pass the scene name for New Game
    }

    public void OnContinueButton()
    {
        StartCoroutine(PlayVideoAndLoadScene(scene_b));  // Pass the scene name for Continue
    }

    public void OnControlsButton()
    {
        SceneManager.LoadScene(2);  // No video for controls button, just load the scene
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    // Coroutine to play the video and load the scene after it's done
    private IEnumerator PlayVideoAndLoadScene(string sceneName)
    {
        // Optional: Set the video URL or assign it in the Inspector
        videoPlayer.url = "https://drive.google.com/uc?export=download&id=1GTgl3EuYgct5y32wxYqM43BidkRWV4z1";

        // Prepare the video
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null; // Wait until the video is fully prepared
        }

        // Play the video
        videoPlayer.Play();

        // Hide the canvas (UI elements) while the video is playing
        canvas.SetActive(false);

        // Wait for the video to finish playing using the loopPointReached event
        videoPlayer.loopPointReached += EndReached;  // Attach the event handler

        // Wait until the video finishes
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
    }

    // Method to handle what happens when the video ends
    private void EndReached(VideoPlayer vp)
    {
        // Load the next scene after the video finishes
        SceneManager.LoadScene(vp.url == videoPlayer.url ? scene_a : scene_b);
    }
}
