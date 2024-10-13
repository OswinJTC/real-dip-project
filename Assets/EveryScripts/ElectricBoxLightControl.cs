using UnityEngine;  // This is the required namespace for Unity components

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
    // Check if lights are already on globally when the scene starts
        if (GameManager.instance.isLightOn)
        {
        // If lights are already on, turn on the bright lights immediately
            TurnOnBrightLights();
        }
        else
        {
        // Start with the room being dark and only the player's spotlight on
            darkDirectionalLight.enabled = true;
            lightDirectionalLight.enabled = false;
            playerSpotlight.enabled = true;
        }
    }


    void Update()
    {
        if (!isLightOn && player != null)
        {
            // Make the spotlight follow the player's position while the room is dark
            playerSpotlight.transform.position = player.position + new Vector3(0, spotlightHeight, 0);
        }

        // Calculate distance between player and the electric box
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if the player is near the electric box and presses "E" to turn on the lights
            if (distanceToPlayer <= detectionRadius && !isLightOn)
            {
                Debug.Log("Player is near the electric box. Press E to turn on the lights.");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E key pressed. Turning on room lights!");
                    TurnOnBrightLights();
                }
            }
        }
    }

    void TurnOnBrightLights()
    {
    // Turn off the dark directional light and the player's spotlight
        darkDirectionalLight.enabled = false;
        playerSpotlight.enabled = false;

    // Turn on the light that brightens the room
        lightDirectionalLight.enabled = true;

    // Update the global GameManager isLightOn variable
        GameManager.instance.isLightOn = true;

        Debug.Log("Room lights are now on.");
    }


    // Optional: Visualize the detection radius in the scene editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
