using UnityEngine;
using UnityEngine.SceneManagement;

public class BabyHouseEntrance : MonoBehaviour
{
    public float detectionRadius = 5f;  // Set a custom detection radius
    public Transform player;            // Reference to the player's transform
    public string nextScene = "BBLivingroomScene";  // Scene to load when entering

    void Update()
    {
        // Calculate the distance between the player and the house
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Log the current distance for debugging purposes
        Debug.Log("Current distance to player: " + distanceToPlayer);

        // Check if the player is within the detection radius and presses the "E" key
        if (distanceToPlayer <= detectionRadius && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed. Player entering the house...");
            EnterHouse();
        }
    }

    void EnterHouse()
    {
        Debug.Log("Entering the house...");

        // Set the player's entry position when entering the house
        Vector3 entryPosition = GetEntryPosition(nextScene);
        GameManager.instance.SetPlayerEntryPosition(entryPosition);

        // Use the TransitionManager to change scenes
        if (TransitionManager.instance != null)
        {
            TransitionManager.instance.ChangeScene(nextScene);
        }
        else
        {
            Debug.LogWarning("TransitionManager instance not found, loading the scene directly.");
            SceneManager.LoadScene(nextScene);
        }
    }

    private Vector3 GetEntryPosition(string sceneToLoad)
    {
        // Define the entry position for entering the living room scene
        if (sceneToLoad == "BBLivingroomScene")
        {
            return new Vector3(-19.6f, 3.63f, 6.27f);
        }

        // Default fallback position if no specific position is defined
        return new Vector3(0f, 0f, 0f);
    }

    // Optional: Visualize the detection radius in the scene editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
