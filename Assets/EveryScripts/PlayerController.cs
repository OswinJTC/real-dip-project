using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runMultiplier = 5f;
    private float currentSpeed;
    private PlayerActionControls playerActionControls;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Camera Settings")]
    public Transform cameraHolder; // Reference to the camera object
    private bool nearFuel = false; // Flag to check if the player is near the fuel

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the camera target to this player if not already assigned
        if (cameraHolder != null)
        {
            Camera2D5DFollow cameraScript = cameraHolder.GetComponent<Camera2D5DFollow>();
            if (cameraScript != null)
            {
                cameraScript.target = transform;
            }
        }
        else
        {
            Debug.LogWarning("Camera Holder is not assigned in PlayerController.");
        }

        // Adjust the player's scale, speed, and visibility based on the current scene
        AdjustPlayerAttributes(SceneManager.GetActiveScene().name);

        // Subscribe to the scene load event to adjust attributes on scene change
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable() => playerActionControls.Enable();
    private void OnDisable() => playerActionControls.Disable();

    void Update()
    {
        Move();

        // Check if the player is in the designated scene, near the fuel, and the monster is not already spawned
        if (SceneManager.GetActiveScene().name == "BedroomScene" && nearFuel && !GameManager.instance.isMonsterSpawned)
        {
            SpawnMonster();
        }
    }

    private void Move()
    {
        // Movement input
        float movementInputX = playerActionControls.Land.Move.ReadValue<float>();
        float movementInputZ = playerActionControls.Land.Move2.ReadValue<float>();
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        currentSpeed = isRunning ? speed * runMultiplier : speed;

        // Update player position
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInputX * currentSpeed * Time.deltaTime;
        currentPosition.z += movementInputZ * currentSpeed * Time.deltaTime;
        transform.position = currentPosition;

        // Animation control
        animator.SetBool("Walk", movementInputX != 0 || movementInputZ != 0);

        //Sprite Flip
        if (movementInputX != 0) // Only change the direction if there is horizontal input
        {
        spriteRenderer.flipX = movementInputX < 0;
        }

    }

    // Adjust player's scale, speed, and visibility based on the scene
    private void AdjustPlayerAttributes(string sceneName)
    {
        // Adjust visibility for specific puzzle scenes
        if (sceneName == "Balloon Puzzle" || sceneName == "Paper Puzzle" || sceneName == "Phone Puzzle" || sceneName == "Bakery Puzzle")
        {
            spriteRenderer.enabled = false; // Hide the player
            Debug.Log("Player made invisible for puzzle scenes.");
        }
        else
        {
            spriteRenderer.enabled = true; // Show the player in other scenes

            // Adjust player scale and speed based on the scene
            if (sceneName == "outsideTerrain")
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                speed = 4f;
                Debug.Log("Player scale and speed adjusted for outsideTerrain.");
            }
            else if (sceneName == "BBLivingroomScene" || sceneName == "BedroomScene" || sceneName == "KitchenScene" || sceneName == "BBBedroomClay" || sceneName == "BBKitchenClay" || sceneName == "BBLRoomClay")
            {
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                speed = 20f;
                Debug.Log("Player scale and speed adjusted for other scenes.");
            }
        }
    }

    // Event handler for when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AdjustPlayerAttributes(scene.name);
    }

    private void SpawnMonster()
    {
        if (GameManager.instance.isMonsterSpawned)
        {
            Debug.Log("Monster is already spawned. Skipping spawn.");
            return;
        }

        GameManager.instance.isMonsterSpawned = true;

        GameObject monster = GameManager.instance.monster;

        if (monster != null)
        {
            Vector3 spawnPosition = new Vector3(-27.41f, 8.34f, 11.58f);
            monster.transform.position = spawnPosition;

            SpriteRenderer renderer = monster.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
                Debug.Log("3D Monster made visible at position: " + spawnPosition);
            }
            else
            {
                Debug.LogWarning("SpriteRenderer not found on the monster GameObject!");
            }
            UIManager.instance.ShowPrompt("WHAT IS THAT!? I need to *THINK* and find a way to get the fuel *(???)*", 10f);
            GameManager.instance.UpdateThinkButton(); // Update the inventory UI to reflect the change
            Debug.Log("3D Monster Spawned at position: " + spawnPosition);
        }
        else
        {
            Debug.LogWarning("3D Monster reference not found in GameManager.");
        }
    }

    // Detect when the player enters the "fuel" area using 3D collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            nearFuel = true;
            Debug.Log("Player is near the fuel.");
        }
    }

    // Detect when the player exits the "fuel" area using 3D collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            nearFuel = false;
            Debug.Log("Player is no longer near the fuel.");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
