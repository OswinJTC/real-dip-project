using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private bool isPlayerNear = false; // Flag to check if the player is near the NPC

    void Update()
    {
        // Check if the player is near the NPC and presses the "E" key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Q) && GameManager.instance.isBreadInUsed)
        {
            InteractWithNPC();
        }
    }

    private void InteractWithNPC()
    {
        // Check if the player has bread in their inventory
        if (GameManager.instance.isBreadActive)
        {
            // Give the key to the player
            GameManager.instance.isKeyActive = true;
            GameManager.instance.UpdateInventoryUI();

            Debug.Log("Player has bread. Key added to inventory.");
            UIManager.instance.ShowPrompt("Player has bread. Key added to inventory.", 2f);
        }
        else
        {
            Debug.Log("Player does not have bread. Cannot receive the key.");
            UIManager.instance.ShowPrompt("Player does not have bread. Cannot receive the key.", 2f);
        }
    }

    // Detect when the player enters the NPC's trigger collider (3D)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the NPC.");
        }
    }

    // Detect when the player exits the NPC's trigger collider (3D)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player is no longer near the NPC.");
        }
    }
}
