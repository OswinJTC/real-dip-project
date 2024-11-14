using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ThreeCorrectVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;  
   

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned to 3CorrectVideoManager!");
        }

    }

    public IEnumerator PlayVideoAndChangeScene(string sceneName)
    {
        // Set your video URL or clip (or configure in Inspector)
        videoPlayer.url = "https://drive.google.com/uc?export=download&id=1GTgl3EuYgct5y32wxYqM43BidkRWV4z1";
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
