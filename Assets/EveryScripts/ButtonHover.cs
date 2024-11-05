using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText; // Reference to the text component

    private Color originalTextColor; // To store the original text color
    private TMP_FontAsset originalFont; // To store the original font
    private float originalFontSize; // To store the original font size

    public Color hoverTextColor = Color.red; // Color of the text when hovering
    public TMP_FontAsset hoverFont; // The font to display when hovering
    public float hoverFontSize = 30f; // The font size when hovering

    void Start()
    {
        // Get the Button's TextMeshProUGUI component (the text)
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing.");
            return;
        }

        // Save the original font, color, and size
        originalTextColor = buttonText.color;
        originalFont = buttonText.font;
        originalFontSize = buttonText.fontSize;
    }

    // Called when the mouse starts hovering over the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the font, color, and size of the text when hovering
        buttonText.color = hoverTextColor; // Change to hover text color
        buttonText.font = hoverFont; // Change to hover font
        buttonText.fontSize = hoverFontSize; // Change to hover font size
    }

    // Called when the mouse stops hovering over the button
    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert the font, color, and size of the text
        buttonText.color = originalTextColor; // Revert to the original text color
        buttonText.font = originalFont; // Revert to the original font
        buttonText.fontSize = originalFontSize; // Revert to the original font size
    }

    // Method to reset text appearance to default
    public void ResetToDefault()
    {
        if (buttonText == null) return;

        buttonText.color = originalTextColor;
        buttonText.font = originalFont;
        buttonText.fontSize = originalFontSize;
    }
}
