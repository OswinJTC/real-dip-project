using UnityEngine;
using UnityEngine.SceneManagement;  // Add this for scene management

public class ElectricBoxLightControl : MonoBehaviour
{
    public Light darkDirectionalLight;      // Light that keeps the room dark initially
    public Light lightDirectionalLight;     // Light that brightens the room
    public Light playerSpotlight;           // Spotlight on the player initially
    public Transform player;                // Reference to the player's position
    public float detectionRadius = 20f;     // Detection radius for the electric box interaction
    public float spotlightHeight = 5f;      // Height of the spotlight above the player

    private bool isLightOn = false;

    void Start()
    {
        if (GameManager.instance.isLightOn)
        {
            TurnOnBrightLights();  // If lights are already on, turn on the bright lights immediately
        }
        else
        {
            darkDirectionalLight.enabled = true;
            lightDirectionalLight.enabled = false;
            playerSpotlight.enabled = true;
        }
    }

    void Update()
    {
        if (!isLightOn && player != null)
        {
            playerSpotlight.transform.position = player.position + new Vector3(0, spotlightHeight, 0);
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius && !isLightOn)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E key pressed. Going to puzzle scene.");
                    GoToPuzzleScene();  // Call the method to transition to the puzzle scene
                    TurnOnBrightLights();
                }
            }
        }
    }

    // Method to change the scene to the puzzle
    void GoToPuzzleScene()
    {
        // Load the puzzle scene (replace "LogicPuzzleScene" with your puzzle scene's actual name)
        SceneManager.LoadScene("LogicPuzzle");
    }

    // Make the method public so it can be called from another script
    public void TurnOnBrightLights()
    {
        darkDirectionalLight.enabled = false;
        playerSpotlight.enabled = false;
        lightDirectionalLight.enabled = true;

        isLightOn = true;
        GameManager.instance.isLightOn = true;

        Debug.Log("Room lights are now on.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
