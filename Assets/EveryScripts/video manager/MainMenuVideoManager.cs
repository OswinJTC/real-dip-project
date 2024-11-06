using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class MainMenuVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // The VideoPlayer component
    public GameObject canvas;        // Reference to the canvas you want to hide
    public AudioSource audioSource;  // Audio source to output video audio

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned to MainMenuVideoManager!");
            return;
        }

        if (canvas == null)
        {
            Debug.LogError("Canvas not assigned to MainMenuVideoManager!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned to MainMenuVideoManager!");
            return;
        }

        // Set the Audio Output Mode to AudioSource explicitly at runtime
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // Check if it successfully set
        if (videoPlayer.audioOutputMode != VideoAudioOutputMode.AudioSource)
        {
            Debug.LogError("Failed to set Audio Output Mode to AudioSource!");
        }
    }

    // Method to play video and trigger scene transition using TransitionManager
    public void PlayNewGameVideo(string sceneName)
    {
        if (videoPlayer != null)
        {
            videoPlayer.url = "https://drive.google.com/uc?export=download&id=1BC9EBOMh4as6GrMUB5yOMesBmWrXYT_t"; // Set the video URL

            videoPlayer.Prepare();  // Prepare the video
            videoPlayer.prepareCompleted += (VideoPlayer vp) => StartCoroutine(PlayVideoAndFade(sceneName));  // Start the coroutine when ready
        }
    }

    // Coroutine to play the video and then fade to the next scene
    private IEnumerator PlayVideoAndFade(string sceneName)
    {
        // Hide the canvas during video playback
        canvas.SetActive(false);

        // Play the video
        videoPlayer.Play();

        // Wait for the video to finish playing
        yield return new WaitUntil(() => !videoPlayer.isPlaying);

        // When the video finishes, call the TransitionManager to fade out and change scene
        if (TransitionManager.instance != null)
        {
            TransitionManager.instance.ChangeScene(sceneName);  // Use fade-out transition to change scene
        }
        else
        {
            Debug.LogWarning("TransitionManager instance not found!");
        }
    }
}
