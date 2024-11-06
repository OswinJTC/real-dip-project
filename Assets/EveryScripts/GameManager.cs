using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance

    // UI Elements
    public Text timerText;
    public GameObject hourglassUIIcon;
    public GameObject gamePanel;
    public GameObject inventoryPanel;
    public GameObject thinkPanel;
    public GameObject pausePanel;
    public GameObject gameoverpanel; // Assign the panel in the Unity Inspector
    public Canvas PersistentCanvas;

    // Player status
    private int playerBlood = 5;
    private float timer = 0f;
    private List<string> inventory = new List<string>();

    // Room cleanliness states
    public bool isLivingRoomClean = false;
    public bool isRoomClean = false;
    public bool isKitchenClean = false;
    public bool isStudyRoomClean = false;

    // Global states
    public bool isLightOn = false;
    public bool isInClayStatus = false;
    public bool isCleaningKit = true; // New variable initialized to true

    // Monster state
    public bool isMonsterSpawned = false;
    public GameObject monster; // Reference to the monster GameObject
    public float monsterSpeed = 3f; // Monster's speed, used in calculations

    // Player references
    public GameObject player; // Reference to the player GameObject
    private Vector3 playerEntryPosition; // Entry position for the player

    // Tutorial Player references
    public GameObject tutPlayer; // Reference to the tutorial player GameObject
    private Vector3 tutPlayerEntryPosition; // Entry position for the tutorial player

    public Vector3? savedPlayerPosition = null; // Nullable to indicate no saved position by default

    // Dictionaries to store positions per scene
    private Dictionary<string, PositionData> playerPositions = new Dictionary<string, PositionData>();
    private Dictionary<string, PositionData> monsterPositions = new Dictionary<string, PositionData>();

    // List of scenes where the monster is inactive
    private List<string> monsterInactiveScenes = new List<string>
    {
        "MainMenuScene",
        "CutsceneScene"
        // Add other scenes where the monster should not be active
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager instance set");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate GameManager destroyed");
            return;
        }

        // Persist UI panels across scenes
        PersistAcrossScenes(gamePanel, inventoryPanel, thinkPanel, pausePanel);
    }

    private void Start()
    {
        HideAllPanels();
        if (gamePanel != null)
        {
            gamePanel.SetActive(true);
        }

        // Check if the player or monster references need to be updated initially
        UpdateReferences();

        // Subscribe to sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        UpdateTimer();

        // Toggle panels with keys
        if (Input.GetKeyDown(KeyCode.I)) TogglePanel(inventoryPanel);
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "outsideTerrain")
        {
            ReduceBlood();
        }

        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentSceneName = scene.name;
        Debug.Log("Scene Loaded: " + currentSceneName);

        // Update references
        UpdateReferences();

        // Restore player and monster positions
        RestorePlayerPosition(currentSceneName);
        RestoreMonsterPosition(currentSceneName);

        if (monsterInactiveScenes.Contains(currentSceneName))
        {
            // Monster should not be active
            if (monster != null)
            {
                monster.SetActive(false);
                Debug.Log("Monster deactivated in scene: " + currentSceneName);
            }
        }
        else
        {
            // Monster should be active
            if (isMonsterSpawned && monster != null)
            {
                monster.SetActive(true);
                Debug.Log("Monster activated in scene: " + currentSceneName);
            }
        }
    }
private void UpdateReferences()
{
    // Define the list of scenes where the player, monster, and PersistentCanvas should not be present
    List<string> playerNonPersistentScenes = new List<string>
    {
        "TutLRoomDScene",
        "TutStudyDScene",
        "TutKitchenDScene",
        "TutStudyCScene",
        "TutLRoomCScene",
        "TutKitchenCScene",
        "TutBRoomDScene",
        "TutBRoomCScene",
        "TutBasementScene",
        "LogicPuzzle",
        "Balloon Puzzle",
        "Phone Puzzle",
        "Paper Puzzle"
    };

    // Check if the current scene is one where the player should not persist
    string currentSceneName = SceneManager.GetActiveScene().name;
    bool isPlayerNonPersistentScene = playerNonPersistentScenes.Contains(currentSceneName);
 
    if (isPlayerNonPersistentScene)
    {
        MakePlayerNonPersistent();
    }
    else
    {
        FindTutPlayer();
        FindPlayer();

        if (monster == null)
        {
            FindMonster();
        }

        if (player != null && !player.activeSelf)
        {
            player.SetActive(true);
            Debug.Log("Persistent player re-enabled.");
        }

        if (tutPlayer != null && !tutPlayer.activeSelf)
        {
            tutPlayer.SetActive(true);
            Debug.Log("Persistent tutPlayer re-enabled.");
        }

        // Ensure the monster is enabled if it should be chasing the player
        if (!monsterInactiveScenes.Contains(currentSceneName) && isMonsterSpawned && monster != null)
        {
            monster.SetActive(true);
            Debug.Log("Persistent monster re-enabled.");
        }
    }
}


    private void MakePlayerNonPersistent()
    {
        if (player != null)
        {
            player.SetActive(false);
            Debug.Log("Persistent player disabled in non-persistent scene.");
        }

        if (tutPlayer != null)
        {
            tutPlayer.SetActive(false);
            Debug.Log("Persistent tutPlayer disabled in non-persistent scene.");
        }

        if (monster != null)
        {
            monster.SetActive(false);
            Debug.Log("Persistent monster disabled in non-persistent scene.");
        }
    }

    private void FindTutPlayer()
    {
        GameObject initialTutPlayer = GameObject.FindWithTag("TutPlayer");

        if (tutPlayer == null)
        {
            if (initialTutPlayer != null)
            {
                tutPlayer = initialTutPlayer;
                DontDestroyOnLoad(tutPlayer);
                Debug.Log("tutPlayer GameObject found and set as persistent in GameManager.");
            }
            else
            {
                Debug.LogWarning("tutPlayer GameObject not found!");
            }
        }
        else
        {
            if (initialTutPlayer != null && initialTutPlayer != tutPlayer)
            {
                Destroy(initialTutPlayer);
                Debug.Log("Duplicate initial tutPlayer destroyed to prevent duplication.");
            }
        }
    }

    public void SetTutPlayerEntryPosition(Vector3 position)
    {
        tutPlayerEntryPosition = position;
        if (tutPlayer == null) FindTutPlayer();
        if (tutPlayer != null) tutPlayer.transform.position = tutPlayerEntryPosition;
    }

    public Vector3 GetTutPlayerEntryPosition() => tutPlayerEntryPosition;

    private void FindPlayer()
    {
        GameObject initialPlayer = GameObject.FindWithTag("Player");

        if (player == null)
        {
            if (initialPlayer != null)
            {
                player = initialPlayer;
                DontDestroyOnLoad(player);
                Debug.Log("Player GameObject found and set as persistent in GameManager.");
            }
            else
            {
                Debug.LogWarning("Player GameObject not found!");
            }
        }
        else
        {
            if (initialPlayer != null && initialPlayer != player)
            {
                Destroy(initialPlayer);
                Debug.Log("Duplicate initial player destroyed to prevent duplication.");
            }
        }
    }

    private void FindMonster()
    {
        // If the monster already exists, destroy any duplicates
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (monsters.Length > 0)
        {
            foreach (GameObject m in monsters)
            {
                MonsterChase monsterChase = m.GetComponent<MonsterChase>();
                if (monsterChase != null && monsterChase != MonsterChase.instance)
                {
                    Destroy(m);
                    Debug.Log("Duplicate Monster destroyed.");
                }
            }
            monster = MonsterChase.instance.gameObject;
            DontDestroyOnLoad(monster);
            Debug.Log("Monster GameObject set in GameManager.");
        }
        else
        {
            Debug.LogWarning("Monster GameObject not found in the scene!");
        }
    }

    public void SetMonsterSpawned(bool spawned)
    {
        isMonsterSpawned = spawned;
        Debug.Log($"Monster spawn state updated: {spawned}");

        if (monster != null)
        {
            monster.SetActive(spawned);
            Debug.Log("Monster visibility updated: " + (spawned ? "visible" : "invisible"));
        }
        else
        {
            Debug.LogWarning("Monster GameObject not found when trying to set visibility!");
        }
    }

    public void SetMonsterPositionNearPlayer()
    {
        if (monster != null && player != null)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 newMonsterPosition = playerPosition - player.transform.forward * 2;
            monster.transform.position = newMonsterPosition;
            Debug.Log("Monster position updated near the player: " + newMonsterPosition);
        }
    }

    public bool GetMonsterSpawned() => isMonsterSpawned;

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        if (timerText != null)
        {
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    private void TogglePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    public void HideAllPanels()
    {
        if (gamePanel != null) gamePanel.SetActive(true);
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (thinkPanel != null) thinkPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    public void AddToInventory(string itemName)
    {
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName);
            Debug.Log($"{itemName} added to inventory.");
        }
    }

    public bool HasItem(string itemName) => inventory.Contains(itemName);

    public void ResetTimer() => timer = 0f;

    public void SetClayStatus(bool isClay)
    {
        isInClayStatus = isClay;
        Debug.Log($"Current status set to: {(isClay ? "Clay" : "Digital")}");
    }

    public bool GetClayStatus() => isInClayStatus;

    public void SetPlayerEntryPosition(Vector3 position)
    {
        playerEntryPosition = position;
        if (player == null)
        {
            FindPlayer();
        }

        if (player != null)
        {
            player.transform.position = playerEntryPosition;
            Debug.Log($"Player position set to: {playerEntryPosition}");
        }
        else
        {
            Debug.LogWarning("Player GameObject not found when trying to set position!");
        }
    }

    public Vector3 GetPlayerEntryPosition() => playerEntryPosition;

    public void ReduceBlood(int amount = 1)
    {
        playerBlood -= amount;
        if (playerBlood < 0)
        {
            playerBlood = 0;
        }

        // Update the eyes accordingly
        for (int i = playerBlood + 1; i <= 5; i++)
        {
            UpdateEyeVisibility($"ClosedEye{i}", false);
            UpdateEyeVisibility($"OpenEye{i}", true);
        }

        Debug.Log($"Player blood reduced by {amount}! Remaining blood: {playerBlood}");

        // Check if player is dead and handle game over
        if (playerBlood <= 0)
        {
            ActivatePanel();
            DeactivateCanvas();
            Debug.Log("Game Over!");
        }
    }

    private void ActivatePanel()
    {
        if (gameoverpanel != null)
        {
            gameoverpanel.SetActive(true); // Activate the panel
            Debug.Log("Panel activated!");
        }
    }

    private void UpdateEyeVisibility(string tag, bool isVisible)
    {
        GameObject eye = GameObject.FindWithTag(tag);
        if (eye != null)
        {
            Image eyeImage = eye.GetComponent<Image>();
            if (eyeImage != null)
            {
                eyeImage.enabled = isVisible;
                Debug.Log($"{tag} visibility set to: {isVisible}");

                // Check if the tag is "OpenEye1" and activate the panel if it is visible
                if (tag == "OpenEye1" && isVisible)
                {
                    ActivatePanel();
                    DeactivateCanvas(); // Call method to deactivate the game panel
                }
            }
        }
        else
        {
            Debug.LogWarning($"{tag} not found.");
        }
    }

    private void DeactivateCanvas()
    {
        if (PersistentCanvas != null)
        {
            PersistentCanvas.gameObject.SetActive(false); // Deactivate the Canvas
            Debug.Log("Canvas deactivated!");
        }
    }

    private void PersistAcrossScenes(params GameObject[] panels)
    {
        foreach (GameObject panel in panels)
        {
            if (panel != null) DontDestroyOnLoad(panel);
        }
    }

    // Class to store position and time
    public class PositionData
    {
        public Vector3 position;
        public float time;

        public PositionData(Vector3 pos, float t)
        {
            position = pos;
            time = t;
        }
    }

    public void SavePlayerPosition(string sceneName)
    {
        if (player != null)
        {
            Vector3 position = player.transform.position;
            float time = Time.time;
            PositionData data = new PositionData(position, time);

            if (playerPositions.ContainsKey(sceneName))
            {
                playerPositions[sceneName] = data;
            }
            else
            {
                playerPositions.Add(sceneName, data);
            }
            Debug.Log("Player position saved for scene " + sceneName + ": " + position);
        }
        else
        {
            Debug.LogWarning("Player reference is null; unable to save position.");
        }
    }

    public void RestorePlayerPosition(string sceneName)
    {
        if (player != null)
        {
            if (playerPositions.ContainsKey(sceneName))
            {
                PositionData data = playerPositions[sceneName];
                Vector3 position = data.position;
                player.transform.position = position;
                Debug.Log("Player position restored for scene " + sceneName + ": " + position);
            }
            else
            {
                Debug.Log("No saved player position for scene " + sceneName);
            }
        }
        else
        {
            Debug.LogWarning("Player reference is null; unable to restore position.");
        }
    }

    public void SaveMonsterPosition(string sceneName)
    {
        if (monster != null)
        {
            Vector3 position = monster.transform.position;
            float time = Time.time;
            PositionData data = new PositionData(position, time);

            if (monsterPositions.ContainsKey(sceneName))
            {
                monsterPositions[sceneName] = data;
            }
            else
            {
                monsterPositions.Add(sceneName, data);
            }
            Debug.Log("Monster position saved for scene " + sceneName + ": " + position);
        }
        else
        {
            Debug.LogWarning("Monster reference is null; unable to save position.");
        }
    }

    public bool MonsterExists()
    {
        return monster != null;
    }

    public void RestoreMonsterPosition(string sceneName)
    {
        if (monster != null && isMonsterSpawned)
        {
            if (monsterPositions.ContainsKey(sceneName))
            {
                PositionData data = monsterPositions[sceneName];
                Vector3 position = data.position;
                monster.transform.position = position;
                Debug.Log("Monster position restored for scene " + sceneName + ": " + position);
            }
            else
            {
                // Optionally, set a default position
                Vector3 defaultPosition = new Vector3(0, 0, 0);
                monster.transform.position = defaultPosition;
                Debug.Log("Monster position set to default for scene " + sceneName);
            }
        }
        else
        {
            Debug.LogWarning("Monster is either not spawned or the reference is null; cannot set position.");
        }
    }
}
