using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MonsterChase : MonoBehaviour
{
    public static MonsterChase instance; // Singleton instance

    private Renderer renderer; // Reference to the monster's Renderer (3D equivalent of SpriteRenderer)
    private bool hasCaughtPlayer = false; // To prevent multiple collision detections
    private bool isMovingTowardsSavedPosition = false; // Track if the monster is moving towards saved position

    [Header("Light Warning Settings")]
    public GameObject lightWarningPanel; // The panel used for light warning effect
    public float warningDistance = 12f;  // Distance threshold for triggering the warning
    public float warningDuration = 1f;   // Duration of the warning effect
    public float warningInterval = 1f; // Interval for flashing effect

    private float logTimer = 0f; // Timer to control log frequency
    private float logInterval = 0.5f; // Interval for log updates (0.5 seconds)

    // List of scenes where the monster and warning panel should be active
    private readonly HashSet<string> allowedScenes = new HashSet<string>
    {
        "BedroomScene", "BBBedroomClay", "BBLivingroomScene", "BBLRoomClay",
        "KitchenScene", "BBKitchenClay", "Bakery Puzzle",
        "Balloon Puzzle", "Paper Puzzle", "Phone Puzzle"
    };

    private void Awake()
    {
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
            if (isMovingTowardsSavedPosition)
            {
                MoveTowardsPlayer();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void GetReferences()
    {
        renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogWarning("Renderer not found on the monster GameObject!");
        }

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
        if (allowedScenes.Contains(scene.name))
        {
            gameObject.SetActive(true); // Enable the monster in allowed scenes
            if (lightWarningPanel != null)
            {
                lightWarningPanel.SetActive(false); // Ensure it's initially off and ready for flashing
            }
            Debug.Log("Monster activated in scene: " + scene.name);
        }
        else
        {
            gameObject.SetActive(false); // Disable the monster in other scenes
            if (lightWarningPanel != null)
            {
                lightWarningPanel.SetActive(false); // Disable the light warning panel in other scenes
            }
            Debug.Log("Monster disabled in scene: " + scene.name);
        }
        
        GetReferences();
    }

    private void MoveTowardsPlayer()
    {
        if (GameManager.instance.GetClayStatus())
        {
            if (GameManager.instance.savedPlayerPosition.HasValue && GameManager.instance.savedMonsterPosition.HasValue)
            {
                Vector3 targetPosition = GameManager.instance.savedPlayerPosition.Value;
                Vector3 startingPosition = GameManager.instance.savedMonsterPosition.Value;

                if (!isMovingTowardsSavedPosition)
                {
                    transform.position = startingPosition;
                    isMovingTowardsSavedPosition = true;
                    SetMonsterVisibility(false);
                }

                MoveMonsterToPosition(targetPosition);
            }
        }
        else
        {
            Transform playerTransform = GameManager.instance.player?.transform;
            if (playerTransform != null)
            {
                Vector3 targetPosition = playerTransform.position;
                MoveMonsterToPosition(targetPosition);
            }
        }
    }

    private void MoveMonsterToPosition(Vector3 targetPosition)
    {
        float monsterSpeed = GameManager.instance.monsterSpeed;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, monsterSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, targetPosition);

        logTimer += Time.deltaTime;
        if (logTimer >= logInterval)
        {
            Debug.Log("Distance between monster and player: " + distance);
            logTimer = 0f;
        }

        if (distance < warningDistance)
        {
            TriggerLightWarning();
        }
    }

    private void SetMonsterVisibility(bool isVisible)
    {
        if (renderer != null)
        {
            renderer.enabled = isVisible;
            Debug.Log("Monster visibility set to: " + isVisible);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCaughtPlayer)
        {
            hasCaughtPlayer = true;
            GameManager.instance.ReduceBlood(1);
            StartCoroutine(ResetHasCaughtPlayer(2f));
        }
    }

    private IEnumerator ResetHasCaughtPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasCaughtPlayer = false;
    }
}
