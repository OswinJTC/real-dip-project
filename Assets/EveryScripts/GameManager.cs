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

    // Player status
    private int playerBlood = 5;
    private float timer = 0f;
    private List<string> inventory = new List<string>();

    // Room cleanliness states
    public bool isLivingRoomClean = false;
    public bool isRoomClean = false;
    public bool isKitchenClean = false;
    public bool isStudyRoomClean = false;

<<<<<<< Updated upstream
    // Global variable to track if the lights are turned on
    public bool isLightOn = false;       // New variable to track whether the lights are turned on
=======
    // Global states
    public bool isLightOn = false;
    public bool isInClayStatus = false;

    // Monster state
    public bool isMonsterSpawned = false;
    public GameObject monster; // Reference to the monster GameObject

    // Player reference
    public GameObject player; // Reference to the player GameObject
>>>>>>> Stashed changes

    private void Awake()
    {
        // Set up Singleton instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    }

    private void Update()
    {
        UpdateTimer();

        // Check if the player or monster references need to be updated based on the current scene
        UpdateReferences();

        // Toggle panels with keys
        if (Input.GetKeyDown(KeyCode.I)) TogglePanel(inventoryPanel);
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "outsideTerrain")
        {
            ReduceBlood();
        }
    }

    private void UpdateReferences()
    {
        // Check if the player reference is missing and we are in the BBLivingroomScene
        if (player == null && SceneManager.GetActiveScene().name == "BBLivingroomScene")
        {
            FindPlayer();
        }

        // Check if the monster reference is missing and we are in the BedroomScene
        if (monster == null && SceneManager.GetActiveScene().name == "BedroomScene")
        {
            FindMonster();
        }
    }

    private void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("Player GameObject found and stored in GameManager.");
        }
        else
        {
            Debug.LogWarning("Player GameObject not found in the BBLivingroomScene!");
        }
    }

    private void FindMonster()
{
    // Check if the monster already exists
    if (monster != null)
    {
        Debug.Log("Monster already exists. Skipping finding a new one.");
        return; // Exit if a monster is already assigned
    }

    // Find the monster in the scene if it's not already assigned
    monster = GameObject.FindWithTag("Monster");
    if (monster != null)
    {
        // Ensure the monster is invisible initially by disabling its MeshRenderer
        MeshRenderer renderer = monster.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
            Debug.Log("Monster GameObject found and made invisible in GameManager.");
        }

        // Make the monster persist across scene loads
        DontDestroyOnLoad(monster);
    }
    else
    {
        Debug.LogWarning("Monster GameObject not found in the BedroomScene!");
    }
}



    public void SetMonsterSpawned(bool spawned)
    {
        isMonsterSpawned = spawned;
        Debug.Log($"Monster spawn state updated: {spawned}");

        if (monster != null)
        {
            // Control the visibility of the monster using its MeshRenderer
            MeshRenderer renderer = monster.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = spawned;
                Debug.Log("Monster visibility updated: " + (spawned ? "visible" : "invisible"));
            }
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
        // Adjust the monster's position to be near the player, e.g., 2 units behind
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

<<<<<<< Updated upstream
    public void ResetTimer()
    {
        timer = 0f;
=======
    public void ResetTimer() => timer = 0f;

    public void SetClayStatus(bool isClay)
    {
        isInClayStatus = isClay;
        Debug.Log($"Current status set to: {(isClay ? "Clay" : "Digital")}");
    }

    public bool GetClayStatus() => isInClayStatus;

    public void SetPlayerEntryPosition(Vector3 position)
    {
        if (player == null)
        {
            FindPlayer();
        }

        if (player != null)
        {
            player.transform.position = position;
            Debug.Log($"Player position set to: {position}");
        }
        else
        {
            Debug.LogWarning("Player GameObject not found when trying to set position!");
        }
    }

    public void ReduceBlood()
    {
        if (playerBlood > 0)
        {
            playerBlood--;
            UpdateEyeVisibility($"ClosedEye{playerBlood + 1}", false);
            UpdateEyeVisibility($"OpenEye{playerBlood + 1}", true);
            Debug.Log($"Player blood reduced! Remaining blood: {playerBlood}");
        }
        else
        {
            Debug.Log("Player has no more blood left!");
            // Handle game over logic here if needed
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
            }
        }
        else
        {
            Debug.LogWarning($"{tag} not found.");
        }
    }

    private void PersistAcrossScenes(params GameObject[] panels)
    {
        foreach (GameObject panel in panels)
        {
            if (panel != null) DontDestroyOnLoad(panel);
        }
>>>>>>> Stashed changes
    }
}
