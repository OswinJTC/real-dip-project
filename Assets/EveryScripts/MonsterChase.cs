using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterChase : MonoBehaviour
{
    public float speed = 3f;  // Speed at which the monster moves
    private Transform player;  // Reference to the player's transform

    void Start()
    {
        // Get the player from the GameManager
        if (GameManager.instance != null && GameManager.instance.player != null)
        {
            player = GameManager.instance.player.transform;
            Debug.Log("Player reference obtained from GameManager.");
        }
        else
        {
            Debug.LogWarning("Player reference in GameManager is null! Attempting to find the player.");
            // Try to find the player in case it's not yet assigned in GameManager
            FindPlayer();
        }
    }

    void Update()
    {
        // Check if the monster should chase the player
        if (!GameManager.instance.isMonsterSpawned)
        {
            // If isMonsterSpawned is false, do not chase
            return;
        }

        // Check if the current scene allows the monster to chase
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "BedroomScene" && currentScene != "BBLivingroomScene" && currentScene != "KitchenScene" && currentScene != "BBHideBRoom")
        {
            // If the scene is not one of the allowed scenes, do not chase
            return;
        }

        // Ensure the player reference is not null
        if (player == null)
        {
            // Try to get the player reference again
            if (GameManager.instance != null && GameManager.instance.player != null)
            {
                player = GameManager.instance.player.transform;
            }
            else
            {
                FindPlayer();
                if (player == null)
                {
                    // Player not found, return
                    return;
                }
            }
        }

        // Move towards the player's position
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            Debug.Log("Player found by MonsterChase script.");
        }
        else
        {
            Debug.LogWarning("Player GameObject not found in the scene by MonsterChase script!");
        }
    }
}
