using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public Transform player;               // Reference to the player
    public float chaseSpeed = 5f;          // Speed of the monster
    public float detectionRadius = 10f;    // Radius where the monster starts chasing the player
    public float cameraDistanceInFront = 3f; // Distance in front of the player for the camera
    public float cameraHeight = 2f;        // Height of the camera above the player
    public Camera mainCamera;              // Reference to the main camera
    public Camera2D5DFollow cameraFollowScript; // Reference to your existing Camera2D5DFollow script
    private bool isChasing = false;        // Flag to track if monster is chasing the player

    private Vector3 initialCameraPosition; // Original camera position
    private Quaternion initialCameraRotation; // Original camera rotation

    void Start()
    {
        // Store the initial camera position and rotation for when the chase ends
        initialCameraPosition = mainCamera.transform.position;
        initialCameraRotation = mainCamera.transform.rotation;
    }

    void Update()
    {
        // Check if the player is within the detection radius to start the chase
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRadius && !isChasing)
        {
            StartChase(); // Start the chase when the player enters the detection radius
        }

        // If the monster is chasing, move towards the player
        if (isChasing)
        {
            ChasePlayer();
            MoveCameraInFrontOfPlayer(); // Move the camera dynamically in front of the player
        }
    }

    void StartChase()
    {
        isChasing = true;
        Debug.Log("Monster started chasing the player!");

        // Disable the 2.5D follow script to take direct control of the camera
        cameraFollowScript.enabled = false;
    }

    void ChasePlayer()
    {
        // Move the monster towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // Optionally, rotate the monster to face the player
        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * chaseSpeed);
    }

    void MoveCameraInFrontOfPlayer()
    {
        // Calculate the position in front of the player
        Vector3 cameraPosition = player.position + player.forward * cameraDistanceInFront + Vector3.up * cameraHeight;
        
        // Move the camera to that position
        mainCamera.transform.position = cameraPosition;

        // Make the camera look at the player's face
        mainCamera.transform.LookAt(player.position + Vector3.up * cameraHeight);
    }

    public void StopChase()
    {
        isChasing = false;
        Debug.Log("Monster stopped chasing the player!");

        // Reset the camera to its original position and rotation
        mainCamera.transform.position = initialCameraPosition;
        mainCamera.transform.rotation = initialCameraRotation;

        // Re-enable the 2.5D follow script to resume normal camera behavior
        cameraFollowScript.enabled = true;
    }
}
