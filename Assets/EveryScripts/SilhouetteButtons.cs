using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SilhouetteButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI displayText;  // Drag your TextMeshProUGUI element here in the inspector

    [Header("Text Customization")]
    public string originalText = "Original Text";  // Default text
    public string hoverText = "Hover Text";        // Text shown on hover

    // When mouse enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        displayText.text = hoverText;
    }

    // When mouse exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        displayText.text = originalText;
    }
}
