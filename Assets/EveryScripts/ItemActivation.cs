using UnityEngine;
using UnityEngine.UI;

public class ItemActivation : MonoBehaviour
{
    public GameObject equippedItemUI;  // The UI element that shows the equipped item
    private bool isItemEquipped = false;

    void Start()
    {
        // Hide the equipped item UI at the start
        equippedItemUI.SetActive(false);
    }

    // Call this when the player clicks the "Activate Item" button
    public void OnActivateItemButton()
    {
        isItemEquipped = true;
        equippedItemUI.SetActive(true);  // Show the item on the UI
        Debug.Log("Item is equipped.");
    }

    // Call this to check if the item is currently equipped
    public bool IsItemEquipped()
    {
        return isItemEquipped;
    }

    // Optionally, unequip the item (if needed)
    public void UnequipItem()
    {
        isItemEquipped = false;
        equippedItemUI.SetActive(false);  // Hide the item on the UI
        Debug.Log("Item is unequipped.");
    }
}
