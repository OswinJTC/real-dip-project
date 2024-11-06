using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MonsterChase : MonoBehaviour
{
    public static MonsterChase instance; // Singleton instance

    private SpriteRenderer renderer; // Reference to the monster's SpriteRenderer
    private bool hasCaughtPlayer = false; // To prevent multiple collision detections

    [Header("Light Warning Settings")]
    public GameObject lightWarningPanel; // The panel used for light warning effect
    public float warningDistance = 12f;  // Distance threshold for triggering the warning
    public float warningDuration = 1f;   // Duration of the warning effect
    public float warningInterval = 1f; // Interval for flashing effect

    private void Awake()
    {
        // Implementing the Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
            Debug.Log("MonsterChase singleton instance created.");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate MonsterChase destroyed.");
            return;
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event
            instance = null;
            Debug.Log("MonsterChase singleton instance destroyed.");
        }
    }

    void Start()
    {
        GetReferences();
    }

    void Update()
    {
        if (GameManager.instance.isMonsterSpawned)
        {
            MoveTowardsPlayer();
        }
    }

    private void GetReferences()
    {
        // Get the SpriteRenderer
        renderer = GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            Debug.LogWarning("SpriteRenderer not found on the monster GameObject!");
        }

        // Find the light warning panel if not set
        if (lightWarningPanel == null)
        {
            GameObject canvas = GameObject.Find("PersistentCanvas");
            if (canvas != null)
            {
                lightWarningPanel = canvas.transform.Find("lightWarningPanel")?.gameObject;
                Debug.Log("LightWarningPanel found at runtime.");
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure references are up-to-date after loading a new scene
        GetReferences();
    }

    private void MoveTowardsPlayer()
    {
        Transform playerTransform = GameManager.instance.player?.transform;
        if (playerTransform == null)
        {
            Debug.LogWarning("Player reference is null. Unable to move monster.");
            return;
        }

        // Move towards the player's position
        float monsterSpeed = GameManager.instance.monsterSpeed;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, monsterSpeed * Time.deltaTime);

        // Check distance to trigger light warning effect
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance < warningDistance)
        {
            TriggerLightWarning();
        }
    }

    void TriggerLightWarning()
    {
        if (lightWarningPanel != null && !lightWarningPanel.activeSelf)
        {
            StartCoroutine(FlashEffect(warningDuration, warningInterval));
        }
    }

    private IEnumerator FlashEffect(float duration, float flashInterval)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            lightWarningPanel.SetActive(true);
            yield return new WaitForSeconds(flashInterval);

            lightWarningPanel.SetActive(false);
            yield return new WaitForSeconds(flashInterval);

            timeElapsed += flashInterval * 2;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasCaughtPlayer)
        {
            hasCaughtPlayer = true;
            GameManager.instance.ReduceBlood(3);
            StartCoroutine(ResetHasCaughtPlayer(2f));
        }
    }

    private IEnumerator ResetHasCaughtPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasCaughtPlayer = false;
    }
}
