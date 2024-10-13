using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public Transform player;               // Reference to the player
    public float chaseSpeed = 5f;          // Speed of the monster
    public float detectionRadius = 10f;    // Radius where the monster starts chasing the player
    private bool isChasing = false;        // Flag to track if monster is chasing the player

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
        }
    }

    void StartChase()
    {
        isChasing = true;
        Debug.Log("Monster started chasing the player!");
    }

    void ChasePlayer()
    {
        // Move the monster towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // Optionally, stop the chase when the monster gets close enough
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < 1f) // If the monster is close enough to the player
        {
            Debug.Log("Monster caught the player!");
            isChasing = false; // Stop chasing after reaching the player (optional)
        }
    }
}
