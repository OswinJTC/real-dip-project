using UnityEngine;
using UnityEngine.SceneManagement;

public class CapsuleMovement : MonoBehaviour
{
    public float speed = 5f; // Default speed, will be adjusted based on the scene
    public Camera mainCamera;

    private Rigidbody rb;
    private bool nearFuel = false; // Flag to check if the player is near the fuel

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Adjust scale and speed based on the initial scene
        AdjustPlayerScaleAndSpeed(SceneManager.GetActiveScene().name);

        // Subscribe to the scene load event to adjust scale and speed on scene change
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Check if the player is in the BedroomScene, near the fuel, and presses 'E'
        if (SceneManager.GetActiveScene().name == "BedroomScene" && nearFuel && Input.GetKeyDown(KeyCode.E) && !GameManager.instance.GetMonsterSpawned())
        {
            SpawnMonster();
        }
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = mainCamera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection * speed * 50f);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, speed);
    }

    // Detect when the player enters the fuel's trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            nearFuel = true;
            Debug.Log("Player is near the fuel.");
        }
    }

    // Detect when the player exits the fuel's trigger collider
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            nearFuel = false;
            Debug.Log("Player is no longer near the fuel.");
        }
    }

    void SpawnMonster()
    {
        // Get the monster reference from the GameManager
        GameObject monster = GameManager.instance.monster;

        if (monster != null)
        {
            // Enable the MeshRenderer to make the monster visible
            MeshRenderer renderer = monster.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
                Debug.Log("Monster made visible.");
            }

            // Update the GameManager's state to reflect that the monster has been spawned
            GameManager.instance.SetMonsterSpawned(true);
            Debug.Log("Monster Spawned.");
        }
        else
        {
            Debug.LogWarning("Monster reference not found in GameManager.");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AdjustPlayerScaleAndSpeed(scene.name);
    }

    void AdjustPlayerScaleAndSpeed(string sceneName)
    {
        if (sceneName == "outsideTerrain")
        {
            // Set the player's scale and speed for the outsideTerrain scene
            transform.localScale = new Vector3(0.5f, 1f, 0.5f);
            speed = 2f; // Slower speed for the outside scene
            Debug.Log("Player scale and speed adjusted for outsideTerrain.");
        }
        else
        {
            // Set the player's scale and speed for any other scene
            transform.localScale = new Vector3(2f, 4f, 2f);
            speed = 10f; // Faster speed for indoor scenes
            Debug.Log("Player scale and speed adjusted for other scenes.");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene load event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
