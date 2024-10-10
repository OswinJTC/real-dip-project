using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;  // For VideoPlayer
using System.Collections; // For coroutine

public class WelcomeToIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Reference to the VideoPlayer component
    public GameObject canvas;         // Reference to the Canvas GameObject

    public void LeaveWelcome()
    {
        StartCoroutine(PlayVideoAndLoadScene());
    }

    private IEnumerator PlayVideoAndLoadScene()
    {

        // Set the video URL (replace with your actual video URL)
        videoPlayer.url = "https://drive.google.com/uc?export=download&id=1GTgl3EuYgct5y32wxYqM43BidkRWV4z1";

        // Prepare the video
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null; // Wait until the video is fully prepared
        }

        // Play the video
        videoPlayer.Play();

        // Deactivate the canvas to hide all UI elements
        canvas.SetActive(false);

        // Wait for the video to finish playing
        yield return new WaitForSeconds((float)videoPlayer.length);

        // After the video ends, load the next scene
        SceneManager.LoadScene("KemasEnvironmentShaderProblem");
    }
}
