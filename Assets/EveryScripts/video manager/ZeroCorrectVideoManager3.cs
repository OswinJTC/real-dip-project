using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ZeroCorrectVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;  
   

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned to 0CorrectVideoManager!");
        }

    }

    public IEnumerator PlayVideoAndChangeScene(string sceneName)
    {
        // Set your video URL or clip (or configure in Inspector)
        videoPlayer.url = "https://drive.google.com/uc?export=download&id=1CyVTziHf3s8A3DhK0Qvv8VJFpLSqFV4U";
        Debug.Log("Video URL set: " + videoPlayer.url);

        // Prepare the video
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null; // Wait until the video is fully prepared
        }


        // Play the video
        videoPlayer.Play();
        Debug.Log("Video playing...");

        // Use a more reliable way to detect the end of the video
        while (videoPlayer.isPlaying)
        {
            yield return null; // Wait until the video is no longer playing
        }

        Debug.Log("Video finished. Transitioning to " + sceneName);

        // Transition to the new scene
        SceneManager.LoadScene(sceneName);  // Load the scene after the video
    }
}
