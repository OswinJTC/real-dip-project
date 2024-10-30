using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public string nextSceneDirty;        // The dirty version of the next scene
    public string nextSceneClean;        // The clean version of the next scene
    public string trapDoorScene;         // The scene for the trap door (for basement)
    public string doorNameForReturning;  // The name to set when returning from the next scene
    private bool isPlayerNear = false;   // Flag to check if the player is near
    private bool isTrapDoor = false;     // Flag to check if this is the trap door
    private static string lastEnteredDoor = "";  // Static variable to track the last entered door

    private TrapdoorVideoManager videoManager;  // Reference to the TrapdoorVideoManager

    void Start()
    {
        // Check if this door is the trap door based on its name
        if (gameObject.name == "trapdoor") // Customize based on the actual trap door object name in the Unity Editor
        {
            isTrapDoor = true;  // Set the flag for trap door
            Debug.Log("Trap door detected on GameObject: " + gameObject.name);

            // Only look for TrapdoorVideoManager if this door is the trap door
            videoManager = FindObjectOfType<TrapdoorVideoManager>();
            if (videoManager == null)
            {
                Debug.LogError("TrapdoorVideoManager not found! This door requires a video manager for trap door functionality.");
            }
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed near door: " + gameObject.name);

            // If it's a trap door interaction, play video and load the basement
            if (isTrapDoor)
            {
                Debug.Log("Player entering the trap door to basement.");
                if (videoManager != null)
                {
                    Debug.Log("Starting trap door video and scene change...");
                    StartCoroutine(videoManager.PlayVideoAndChangeScene(trapDoorScene));  // Use VideoManager to play video and change scene
                }
                else
                {
                    Debug.LogWarning("TrapdoorVideoManager is missing! Cannot play video for trap door.");
                }
                return;
            }

            // Check if the lights are on before allowing the player to enter other doors
            if (!GameManager.instance.isLightOn)
            {
                Debug.LogWarning("The lights are still off! You cannot enter the door until the lights are on.");
                return; // Exit early if the lights are not turned on
            }

            // Determine which version of the scene to load (clean or dirty)
            string sceneToLoad = DetermineSceneToLoad();

            // Check if the scene to load is set correctly
            if (string.IsNullOrEmpty(sceneToLoad))
            {
                Debug.LogWarning("Scene is not set for the door: " + gameObject.name);
                return; // Prevent loading if no scene is set
            }

            Debug.Log("Loading scene: " + sceneToLoad);
            lastEnteredDoor = doorNameForReturning; // Store the door name for returning
            Debug.Log("Last entered door set to: " + lastEnteredDoor);

            // Use TransitionManager to load the scene with fade effect
            TransitionManager.instance.ChangeScene(sceneToLoad);
        }
    }

    // Determine whether to load the clean or dirty version of the scene
    string DetermineSceneToLoad()
    {
        switch (nextSceneDirty)
        {
            case "TutLRoomDScene":  // Living room
                return GameManager.instance.isLivingRoomClean ? nextSceneClean : nextSceneDirty;

            case "TutBRoomDScene":  // Room
                return GameManager.instance.isRoomClean ? nextSceneClean : nextSceneDirty;

            case "TutKitchenDScene":  // Kitchen
                return GameManager.instance.isKitchenClean ? nextSceneClean : nextSceneDirty;

            case "TutStudyDScene":  // Study room
                return GameManager.instance.isStudyRoomClean ? nextSceneClean : nextSceneDirty;

            default:
                Debug.LogWarning("No matching room state found for: " + nextSceneDirty);
                return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;  // Player is near the door
            Debug.Log("Player is near the door: " + gameObject.name);

            // Check if this door is the trap door
            if (gameObject.name == "trapdoor")
            {
                isTrapDoor = true;  // Set the flag for trap door
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;  // Player is no longer near the door
            Debug.Log("Player has left the door: " + gameObject.name);
        }
    }

    // Method to get the last entered door name (used by SceneSpawnManager)
    public static string GetLastEnteredDoor()
    {
        return lastEnteredDoor;
    }
}
