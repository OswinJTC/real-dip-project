using UnityEngine;
using UnityEngine.UI;

public class MonsterChase : MonoBehaviour
{
    public Transform player;                   // Reference to the player
    public float chaseSpeed = 5f;              // Speed of the monster when chasing
    public float roamRadius = 5f;              // Radius within which the monster roams
    public float detectionRadius = 10f;        // Radius where the monster starts chasing the player
    public float roamSpeed = 2f;               // Speed of the monster when roaming
    public Camera2D5DFollow cameraFollowScript; // Reference to the 2.5D camera follow script
    private Vector3 roamPosition;              // The target position for roaming
    private bool isChasing = false;            // Flag to track if the monster is chasing the player

    public Renderer monsterRenderer;           // Renderer to change the monster's color
    public Color normalColor = Color.green;    // Default color when roaming
    public Color chaseColor = Color.red;       // Color when chasing the player

    public Text healthText;                    // UI Text element to display health

    void Start()
    {
        roamPosition = GetRoamingPosition();   // Set initial roaming position
        UpdateHealthUI();                      // Initialize health display with global health value
        monsterRenderer.material.color = normalColor; // Set initial monster color
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Change color and start chasing when the player is close
        if (distanceToPlayer <= detectionRadius && !isChasing)
        {
            StartChase(); // Start the chase
        }

        // Handle roaming when not chasing
        if (!isChasing)
        {
            Roam();
        }

        // Move towards the player if chasing
        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void Roam()
    {
        if (Vector3.Distance(transform.position, roamPosition) < 1f)
        {
            roamPosition = GetRoamingPosition();  // Get new random position
        }

        // Move towards the roaming position
        Vector3 direction = (roamPosition - transform.position).normalized;
        transform.position += direction * roamSpeed * Time.deltaTime;
    }

    Vector3 GetRoamingPosition()
    {
        // Calculate a random point within a small radius
        return transform.position + (Random.insideUnitSphere * roamRadius);
    }

    void StartChase()
    {
        isChasing = true;
        Debug.Log("Monster started chasing the player!");

        // Change the monster's color to indicate chase
        monsterRenderer.material.color = chaseColor;
    }

    void ChasePlayer()
    {
        // Move the monster towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // Rotate the monster to face the player
        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * chaseSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Monster touches the player, reduce global health from GameManager
            GameManager.instance.playerHealth -= 3;
            UpdateHealthUI();

            if (GameManager.instance.playerHealth <= 0)
            {
                EndChase(); // End the chase if health is 0
            }
        }
    }

    void EndChase()
    {
        isChasing = false;
        Debug.Log("Monster stopped chasing the player!");
        // Reset monster's color back to normal
        monsterRenderer.material.color = normalColor;
    }

    void UpdateHealthUI()
    {
        // Update the health display in the UI using global health from GameManager
        healthText.text = "Health: " + GameManager.instance.playerHealth.ToString();
    }
}
