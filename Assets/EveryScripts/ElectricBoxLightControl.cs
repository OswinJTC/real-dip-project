using UnityEngine;

public class ElectricBoxLightControl : MonoBehaviour
{
    public Light darkDirectionalLight;
    public Light lightDirectionalLight;
    public Light playerSpotlight;
    public Transform player;
    public float detectionRadius = 20f;   // Set the detection radius for larger range

    private bool isPlayerNear = false;
    private bool isLightOn = false;

    void Start()
    {
        darkDirectionalLight.enabled = true;
        lightDirectionalLight.enabled = false;
        playerSpotlight.enabled = true;
    }

    void Update()
    {
        // Calculate distance between player and the electric box
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection radius and presses "E"
        if (distanceToPlayer <= detectionRadius && !isLightOn)
        {
            Debug.Log("Player is near the electric box. Press E to turn on the lights.");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed. Turning on bright lights!");
                TurnOnBrightLights();
            }
        }
    }

    void TurnOnBrightLights()
    {
        darkDirectionalLight.enabled = false;
        playerSpotlight.enabled = false;
        lightDirectionalLight.enabled = true;
        isLightOn = true;
        Debug.Log("Bright lights are now on.");
    }

    // Optional: Visualize the detection radius in the scene editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
