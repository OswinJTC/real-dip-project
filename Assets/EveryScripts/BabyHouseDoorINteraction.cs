using UnityEngine;
using UnityEngine.SceneManagement;

public class BabyHouseDoorInteraction : MonoBehaviour
{
    public string nextScenePixel;        // The pixel version of the next scene
    public string nextSceneClay;         // The clay version of the next scene
    public string doorNameForReturning;  // The name to set when returning from the next scene
    private bool isPlayerNear = false;   // Flag to check if the player is near
    private static string lastEnteredDoor = "";  // Static variable to track the last entered door

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Determine which version of the scene to load (pixel or clay)
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

            // Set the entry position for the player based on the destination
            Vector3 entryPosition = GetEntryPosition(sceneToLoad, lastEnteredDoor);
            GameManager.instance.SetPlayerEntryPosition(entryPosition);

            // Use TransitionManager to load the scene with a fade effect
            TransitionManager.instance.ChangeScene(sceneToLoad);
        }
    }

    // Determine whether to load the pixel or clay version of the scene
    private string DetermineSceneToLoad()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is not initialized!");
            return null;
        }

        return GameManager.instance.GetClayStatus() ? nextSceneClay : nextScenePixel;
    }

    // Determine the entry position based on the scene being loaded and the last entered door
    private Vector3 GetEntryPosition(string sceneToLoad, string lastEnteredDoor)
    {
        if (lastEnteredDoor == "kithcendoortombroom" && (sceneToLoad == "BedroomScene" || sceneToLoad == "BBBedroomClay"))
        {
            return new Vector3(-18f, 0.3f, 22.7f); 
        }
        else if (lastEnteredDoor == "bedroomdoortokitchen" && (sceneToLoad == "KitchenScene" || sceneToLoad == "BBKitchenClay"))
        {
            return new Vector3(-20.6f, 4f, 16.13f); 
        }
        else if (lastEnteredDoor == "bedroomdoortolroom" && (sceneToLoad == "BBLivingroomScene" || sceneToLoad == "BBLRoomClay"))
        {
            return new Vector3(26.24f, 4f, 22f); 
        }
        else if (sceneToLoad == "KitchenScene" || sceneToLoad == "BBKitchenClay")// Position when entering the kitchen
        {
            return new Vector3(-22.79f, 0.08f, 4.78f); 
        }
        else if (sceneToLoad == "BBLivingroomScene" || sceneToLoad == "BBLRoomClay")// Position when entering the livingroom from bedroom
        {
            return new Vector3(31.67f, 4f, 10f); 
        }
        else if (sceneToLoad == "BedroomScene" || sceneToLoad == "BBBedroomClay")// Position when entering the bedroom
        {
            return new Vector3(21.2f, 0.3f, 22.7f); 
        }

        // Default position for living room if no specific position is set
        return new Vector3(0f, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;  // Player is near the door
            Debug.Log("Player is near the door: " + gameObject.name);
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
