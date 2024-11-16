using UnityEngine;
using UnityEngine.UI;
using TMPro; // Make sure you have this namespace for TextMeshPro
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image targetImage; // The image to change
    public Sprite hoverImage; // The new image when hovered
    public Sprite originalImage; // The original image
    public Vector2 originalSize = new Vector2(100, 100); // The original size of the image
    public Vector2 hoverSize = new Vector2(150, 150); // The new size when hovered

    public Image secondTargetImage; // The second image to change
    public Sprite buttonPressImage; // The new image when the button is pressed
    public Vector2 secondImageOriginalSize = new Vector2(100, 100); // Original size of the second image
    public Vector2 secondImagePressSize = new Vector2(200, 200); // New size when button is pressed

    // New fields for changing text
    public TextMeshProUGUI targetText; // The text to change
    public string hoverText = "Hovered!"; // The text when hovered
    public string originalText = "Original Text"; // The original text

    // When the pointer enters the button (hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.sprite = hoverImage; // Change the image
            targetImage.rectTransform.sizeDelta = hoverSize; // Change the size of the image
        }

        // Change the text
        if (targetText != null)
        {
            targetText.text = hoverText; // Change the text
        }
    }

    // When the pointer exits the button (no longer hovering)
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.sprite = originalImage; // Revert to the original image
            targetImage.rectTransform.sizeDelta = originalSize; // Revert to the original size
        }

        // Revert the text
        if (targetText != null)
        {
            targetText.text = originalText; // Revert to the original text
        }
    }

    // Method to change the second image when the button is pressed
    public void OnButtonPress()
    {
        if (secondTargetImage != null)
        {
            Debug.Log("Changing second image to: " + buttonPressImage.name);
            secondTargetImage.sprite = buttonPressImage; // Change the second image

            // Change the size of the second image
            secondTargetImage.rectTransform.sizeDelta = secondImagePressSize; // Change size to the new size

            if (buttonPressImage.name == "Cleaning Kit")
        {
            GameManager.instance.isBreadInUsed = false;
            GameManager.instance.isKeyInUsed = false;
            GameManager.instance.isCleaningKitInUsed = true;
        }
        else if (buttonPressImage.name == "Bread")
        {
            GameManager.instance.isBreadInUsed = true;
            GameManager.instance.isKeyInUsed = false;
            GameManager.instance.isCleaningKitInUsed = false;
        }
        else if (buttonPressImage.name == "Key 10 - GOLD -")
        {
            GameManager.instance.isBreadInUsed = false;
            GameManager.instance.isKeyInUsed = true;
            GameManager.instance.isCleaningKitInUsed = false;
        }
        }
        else
        {
            Debug.LogWarning("secondTargetImage is not assigned!");
        }
    }
}
