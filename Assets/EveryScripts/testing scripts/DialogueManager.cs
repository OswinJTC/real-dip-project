using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueCanvas;  // The canvas that holds the dialogue UI
    public Text dialogueText;  // The text component that displays dialogue
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
        dialogueCanvas.SetActive(false);  // Initially hide the dialogue canvas

        // Assign the button click listener once at the start
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    // Start the dialogue with a callback to be called when dialogue ends
    public void StartDialogue(DialogueLine[] lines, Action onDialogueEnd = null)
    {
        dialogueLines = lines;

        dialogueCanvas.SetActive(true);  // Show the dialogue canvas
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

        // Set the dialogue text
        dialogueText.text = currentLine.text;

        // Update the text alignment based on the speaker
        if (currentLine.speaker == "Player")
        {
            dialogueText.alignment = TextAnchor.MiddleLeft;  // Align text to the left for the player
        }
        else if (currentLine.speaker == "Boss")
        {
            dialogueText.alignment = TextAnchor.MiddleRight;  // Align text to the right for the boss
        }
    }

    void EndDialogue()
    {
        dialogueCanvas.SetActive(false);  // Hide the dialogue canvas when the conversation ends

        // Call the callback function if it exists
        onDialogueEndCallback?.Invoke();
    }
}
