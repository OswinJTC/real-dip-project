using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    public Transform doorToStudySpawnPoint;    // Set spawn points for each door
    public Transform doorToKitchenSpawnPoint;
    public Transform doorToRoomSpawnPoint;
    public GameObject playerPrefab;            // Reference to the player prefab if you instantiate them

    void Start()
    {
        string lastEnteredDoor = DoorInteraction.GetLastEnteredDoor(); // Get the last entered door
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null && playerPrefab != null) // If no player found in the scene, instantiate a new one
        {
            player = Instantiate(playerPrefab);
        }

        // Set the player's position based on the last entered door
        switch (lastEnteredDoor)
        {
            case "doortostudy":
                player.transform.position = doorToStudySpawnPoint.position;
                player.transform.rotation = doorToStudySpawnPoint.rotation;
                break;
            case "doortokitchen":
                player.transform.position = doorToKitchenSpawnPoint.position;
                player.transform.rotation = doorToKitchenSpawnPoint.rotation;
                break;
            case "doortoroom":
                player.transform.position = doorToRoomSpawnPoint.position;
                player.transform.rotation = doorToRoomSpawnPoint.rotation;
                break;
            default:
                Debug.Log("Spawn position not found for door: " + lastEnteredDoor);
                break;
        }
    }
}
