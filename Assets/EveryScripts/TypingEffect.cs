using UnityEngine;
using TMPro; // Import for using TextMeshPro components
using System.Collections;

public class TypingEffectTMP : MonoBehaviour
{
    public TMP_Text typingText; // Assign your TextMeshPro component in the Inspector
    public string fullText; // The full text to display
    public float typingSpeed = 0.05f; // Speed of the typing effect
    public TMP_FontAsset fontAsset; // Assign your font asset in the Inspector

    void Start()
    {
        typingText.text = ""; // Start with empty text
        if (fontAsset != null)
        {
            typingText.font = fontAsset; // Set the font asset if assigned
        }
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            typingText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(typingSpeed); // Wait for the specified typing speed
        }
    }
}
