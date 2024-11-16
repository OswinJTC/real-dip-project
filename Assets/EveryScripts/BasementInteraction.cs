using UnityEngine;
using UnityEngine.SceneManagement;

public class BasementInteraction : MonoBehaviour
{
    public float detectionRadius = 3f;  // The radius within which the player can interact with the hourglass
    public Transform player;            // Reference to the player's transform
    public string nextScene = "outsideTerrain";  // Scene to load when exiting the basement

    private BasementVideoManager videoManager;  // Reference to the video manager

    void Start()
    {
        // Find the video manager in the scene
        videoManager = FindObjectOfType<BasementVideoManager>();
        if (videoManager == null)
        {
            Debug.LogError("BasementVideoManager not found! This interaction requires a video manager for hourglass functionality.");
        }
        else
        {
            Debug.Log("BasementVideoManager successfully found in the scene.");
        }

        // Confirm that player reference is assigned
        if (player == null)
        {
            Debug.LogError("Player reference not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Calculate the distance between player and hourglass
        if (player == null)
        {
            Debug.LogError("Player reference is null!");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log("Current distance to player: " + distanceToPlayer);

        // If the player is within range and presses "E" to pick up the hourglass
        if (distanceToPlayer <= detectionRadius && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed within range. Attempting to exit basement...");
            ExitBasement();
        }
    }

    void ExitBasement()
{
    Debug.Log("Attempting to exit basement...");

    // Check if GameManager instance is null
    if (GameManager.instance == null)
    {
        Debug.LogError("GameManager instance is null! Make sure GameManager is present in the scene.");
        return;
    }

    // Set the player's entry position when exiting the basement
    Vector3 entryPosition = GetEntryPosition(nextScene);
    GameManager.instance.SetPlayerEntryPosition(entryPosition);
    Debug.Log("Player entry position set to: " + entryPosition);

    // Activate the hourglass item in the GameManager
    GameManager.instance.isHourglassActive = true;
    GameManager.instance.UpdateInventoryUI(); // Update the inventory UI to reflect the change
    GameManager.instance.UpdateHourglassAndEye();

    // Check if videoManager is null before trying to use it
    if (videoManager != null)
    {
        Debug.Log("Playing video before exiting the basement...");
        StartCoroutine(videoManager.PlayVideoAndChangeScene(nextScene));
    }
    else
    {
        Debug.LogWarning("BasementVideoManager is missing! Loading the scene directly.");
        SceneManager.LoadScene(nextScene);
    }
}


    private Vector3 GetEntryPosition(string sceneToLoad)
    {
        // Define the entry position for the next scene when exiting the basement
        if (sceneToLoad == "outsideTerrain")
        {
            return new Vector3(-15.03948f, 1.15693f, 133.628f);  // Customize as needed
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
