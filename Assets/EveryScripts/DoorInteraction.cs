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
        videoManager = FindObjectOfType<TrapdoorVideoManager>();
        if (videoManager == null)
        {
            Debug.LogError("Now in this room scene; TrapdoorVideoManager not found!");
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // If it's a trap door interaction, play video and load the basement
            if (isTrapDoor)
            {
                Debug.Log("Player entering the trap door to basement.");
                StartCoroutine(videoManager.PlayVideoAndChangeScene(trapDoorScene));  // Use VideoManager to play video and change scene
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

            Debug.Log("Player pressed E near the door: " + gameObject.name);
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

            // Check if the door is a trap door
            if (gameObject.name == "trapdoor") // You can customize this based on the actual name of the trap door object
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
        }
    }

    // Method to get the last entered door name (used by SceneSpawnManager)
    public static string GetLastEnteredDoor()
    {
        return lastEnteredDoor;
    }
}
