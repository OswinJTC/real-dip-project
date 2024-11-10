using UnityEngine;
using UnityEngine.Video;

public class StudyRoomVideoManager  : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // The VideoPlayer component
    public GameObject canvas;  // Reference to the canvas you want to hide

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned to StudyRoomVideoManager!");
        }

        if (canvas == null)
        {
            Debug.LogError("Canvas not assigned to StudyRoomVideoManager!");
        }
    }

    // Method to play the video
    public void PlayVideo()
    {
        if (videoPlayer != null)
        {
            // Set your video URL or clip
            videoPlayer.url = "https://drive.google.com/uc?export=download&id=1GTgl3EuYgct5y32wxYqM43BidkRWV4z1";
            Debug.Log("Video URL set: " + videoPlayer.url);

            videoPlayer.Prepare();
            Debug.Log("Preparing video...");

            // Attach an event handler for when the video is prepared
            videoPlayer.prepareCompleted += OnVideoPrepared;
        }
        else
        {
            Debug.LogError("No VideoPlayer assigned.");
        }
    }

    // This is called when the video is prepared and ready to play
    private void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("Video prepared and ready to play.");

        // Hide the canvas before playing the video
        if (canvas != null)
        {
            canvas.SetActive(false);  // Hide the canvas
        }

        videoPlayer.Play();  // Play the video

        // Re-enable the canvas when the video finishes
        videoPlayer.loopPointReached += EndVideo;  // This is called when the video finishes playing
    }

    // This method is called when the video finishes playing
    private void EndVideo(VideoPlayer vp)
    {
        Debug.Log("Video finished playing.");
        UIManager.instance.ShowPrompt("Find out the trapdoor to enter the basement...", 5f);

        // Show the canvas again after the video ends
        if (canvas != null)
        {
            canvas.SetActive(true);  // Show the canvas
        }
    }
}
