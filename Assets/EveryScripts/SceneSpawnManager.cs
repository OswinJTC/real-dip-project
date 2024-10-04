using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    // Set spawn points for each door (both for leaving and returning to the original room)
    public Transform doorToStudySpawnPoint;
    public Transform doorToKitchenSpawnPoint;
    public Transform doorToRoomSpawnPoint;
    
    // Spawn points when returning to the original room
    public Transform doorToLivingRoomFromStudy;
    public Transform doorToLivingRoomFromKitchen;
    public Transform doorToLivingRoomFromRoom;

    public GameObject playerPrefab;  // Reference to the player prefab if needed

    void Start()
    {
        string lastEnteredDoor = DoorInteraction.GetLastEnteredDoor(); // Get the last entered door
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null && playerPrefab != null) // If no player found in the scene, instantiate a new one
        {
            player = Instantiate(playerPrefab);
        }

        // Adjust player position based on the last entered door
        switch (lastEnteredDoor)
        {
            // For entering other rooms
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

            // For returning to the original room (living room)
            case "doorToLivingRoomFromStudy":
                player.transform.position = doorToLivingRoomFromStudy.position;
                player.transform.rotation = doorToLivingRoomFromStudy.rotation;
                break;
            case "doorToLivingRoomFromKitchen":
                player.transform.position = doorToLivingRoomFromKitchen.position;
                player.transform.rotation = doorToLivingRoomFromKitchen.rotation;
                break;
            case "doorToLivingRoomFromRoom":
                player.transform.position = doorToLivingRoomFromRoom.position;
                player.transform.rotation = doorToLivingRoomFromRoom.rotation;
                break;

            default:
                Debug.Log("Spawn position not found for door: " + lastEnteredDoor);
                break;
        }
    }
}
