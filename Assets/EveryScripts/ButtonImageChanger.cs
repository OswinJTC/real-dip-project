using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonImageChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [System.Serializable]
    public class ImageChange
    {
        public Image defaultImage;  // The default Image component to show normally
        public Image hoverImage;    // The hover Image component to show on hover
    }

    public ImageChange[] imagesToChange;  // Array of image pairs to change

    // Triggered when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (ImageChange imgChange in imagesToChange)
        {
            if (imgChange.defaultImage != null && imgChange.hoverImage != null)
            {
                // Disable the default image and enable the hover image
                imgChange.defaultImage.gameObject.SetActive(false);
                imgChange.hoverImage.gameObject.SetActive(true);
            }
        }
    }

    // Triggered when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (ImageChange imgChange in imagesToChange)
        {
            if (imgChange.defaultImage != null && imgChange.hoverImage != null)
            {
                // Enable the default image and disable the hover image
                imgChange.defaultImage.gameObject.SetActive(true);
                imgChange.hoverImage.gameObject.SetActive(false);
            }
        }
    }

    // Method to reset images to their default state
    public void ResetToDefault()
    {
        foreach (ImageChange imgChange in imagesToChange)
        {
            if (imgChange.defaultImage != null && imgChange.hoverImage != null)
            {
                // Enable the default image and disable the hover image
                imgChange.defaultImage.gameObject.SetActive(true);
                imgChange.hoverImage.gameObject.SetActive(false);
            }
        }
    }
}
