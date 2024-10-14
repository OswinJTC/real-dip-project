using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEntrance : MonoBehaviour
{
    public float detectionRadius = 5f;  // Set a custom detection radius
    public Transform player;            // Reference to the player's transform

    void Update()
    {
        // Calculate the distance between the player and the house
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Log the current distance for debugging purposes
        Debug.Log("Current distance to player: " + distanceToPlayer);

        // Check if the player is within the detection radius and presses the "E" key
        if (distanceToPlayer <= detectionRadius && Input.GetKeyDown(KeyCode.E))
        {
            EnterHouse();
        }
    }

    void EnterHouse()
    {
        Debug.Log("Entering the house...");

        // Use the TransitionManager to fade out and change scenes
        if (TransitionManager.instance != null)
        {
            // Call the fade transition to change scene to "TutLRoomDScene"
            TransitionManager.instance.ChangeScene("TutLRoomDScene");
        }
        else
        {
            Debug.LogWarning("TransitionManager instance not found, loading the scene directly.");
            SceneManager.LoadScene("TutLRoomDScene");  // Fallback to direct scene loading if TransitionManager is not available
        }
    }

    // Optional: Visualize the detection radius in the scene editor
    void OnDrawGizmosSelected()
    {
        // Draw a wireframe sphere in the Scene view to visualize the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
