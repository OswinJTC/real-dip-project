using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MCQTrigger : MonoBehaviour
{
    public GameObject mcqPanel;  // The panel containing the button and blur background
    public VideoPlayer videoPlayer;  // Reference to the VideoPlayer component
    public DialogueManager dialogueManager;  // Reference to the DialogueManager script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mcqPanel.SetActive(true);  // Show the MCQ panel with button and blur
            Time.timeScale = 0;  // Pause the game when MCQ appears
        }
    }

    // Call this function when the player presses the button after completing the MCQ
    public void LoadNewScene()
    {
        Time.timeScale = 1;  // Resume the game
        StartCoroutine(PlayVideoThenShowDialogue());
    }

    IEnumerator PlayVideoThenShowDialogue()
    {
        // Hide the MCQ panel (button and blur background)
        mcqPanel.SetActive(false);

        // Set the direct URL of the video hosted on Google Drive
        videoPlayer.url = "https://drive.google.com/uc?export=download&id=1GTgl3EuYgct5y32wxYqM43BidkRWV4z1";

        // Wait until the video is prepared
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;  // Wait until the video is fully prepared
        }

        // Play the video
        videoPlayer.Play();

        // Wait for the video to finish playing
        yield return new WaitForSeconds((float)videoPlayer.length);

        // Stop and hide the video player after the video finishes
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);  // Hide the video player GameObject

        // Start the conversation dialogue after the video
        DialogueManager.DialogueLine[] lines = new DialogueManager.DialogueLine[]
        {
            new DialogueManager.DialogueLine { speaker = "Player", text = "What happened here?" },
            new DialogueManager.DialogueLine { speaker = "Boss", text = "The warehouse has been abandoned for years." },
            new DialogueManager.DialogueLine { speaker = "Player", text = "Let's search for clues." },
            new DialogueManager.DialogueLine { speaker = "Boss", text = "Be careful, it's not safe." }
        };

        // Start the dialogue and pass the callback to load the next scene
        dialogueManager.StartDialogue(lines, OnDialogueEnd);
    }

    // Callback to be called when the dialogue ends
    void OnDialogueEnd()
    {
        // Load the next scene after the dialogue ends
        SceneManager.LoadScene("Warehouse");  // Load the Warehouse scene
    }
}
