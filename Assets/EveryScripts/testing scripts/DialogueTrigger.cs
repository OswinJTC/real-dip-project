using UnityEngine;
using UnityEngine.UI; // Make sure to include this if you are using UI elements
using System;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas;  // The canvas that holds the dialogue UI
    public Text playerText;  // The text component for Player dialogue
    public Text bossText;    // The text component for Boss dialogue
    public Button nextButton;  // The button to continue the dialogue

    private DialogueLine[] dialogueLines;  // Array of dialogue lines (with speaker and text)
    private int currentLineIndex = 0;  // Tracks the current dialogue line
    private Action onDialogueEndCallback;  // Callback to be executed when dialogue ends

    // Struct to hold dialogue line and speaker information
    [Serializable]
    public struct DialogueLine
    {
        public string speaker;  // Speaker identifier ("Player" or "Boss")
        public string text;     // Dialogue text
    }

    void Start()
    {
    
        // Assign the button click listener once at the start
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    // Start the dialogue with a callback to be called when dialogue ends
    public void StartDialogue(DialogueLine[] lines, Action onDialogueEnd = null)
    {
        dialogueLines = lines;

        dialogueCanvas.SetActive(true);  // Show the dialogue canvas
        Debug.Log("Dialogue canvas set to active");
        currentLineIndex = 0;
        UpdateDialogueUI();  // Update the UI with the first line

        // Store the callback to be executed at the end of the dialogue
        onDialogueEndCallback = onDialogueEnd;
    }

    public void DisplayNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            UpdateDialogueUI();  // Update the dialogue UI for the next line
        }
        else
        {
            EndDialogue();  // End the dialogue if all lines are displayed
        }
    }

    // Update the UI based on the current dialogue line
    void UpdateDialogueUI()
    {
        DialogueLine currentLine = dialogueLines[currentLineIndex];

        if (currentLine.speaker == "Player")
        {
            playerText.text = currentLine.text;
            playerText.gameObject.SetActive(true);  // Show player text
            bossText.gameObject.SetActive(false);    // Hide boss text
        }
        else if (currentLine.speaker == "Boss")
        {
            bossText.text = currentLine.text;
            bossText.gameObject.SetActive(true);     // Show boss text
            playerText.gameObject.SetActive(false);  // Hide player text
        }
    }

    void EndDialogue()
    {
        dialogueCanvas.SetActive(false);  // Hide the dialogue canvas when the conversation ends

        // Call the callback function if it exists
        onDialogueEndCallback?.Invoke();
    }
}
