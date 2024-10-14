using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image hourglassIcon;  // Reference to the hourglass icon in the UI

    void Update()
    {
        // Check if the hourglass is in the inventory
        if (GameManager.instance != null && GameManager.instance.HasItem("Hourglass"))
        {
            hourglassIcon.enabled = true;  // Show the icon if the hourglass is in the inventory
        }
        else
        {
            hourglassIcon.enabled = false;  // Hide the icon if it's not in the inventory
        }
    }
}
