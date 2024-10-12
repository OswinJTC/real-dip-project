using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstIntroduceStory : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;  // Reference to the DialogueTrigger component

    void Start()
    {
        Debug.Log("FirstIntroduceStory Start called");
        
        // Define your dialogue lines
        DialogueTrigger.DialogueLine[] lines = new DialogueTrigger.DialogueLine[]
        {
            new DialogueTrigger.DialogueLine { speaker = "Player", text = "What is this place?" },
            new DialogueTrigger.DialogueLine { speaker = "Boss", text = "This is where it all began." },
            new DialogueTrigger.DialogueLine { speaker = "Player", text = "I need to find out more." },
            new DialogueTrigger.DialogueLine { speaker = "Boss", text = "Be cautious; not everything is as it seems." },
            // Add more lines as needed
        };

        // Start the dialogue
        dialogueTrigger.StartDialogue(lines);
    }
}


