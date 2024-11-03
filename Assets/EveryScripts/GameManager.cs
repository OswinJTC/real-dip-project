using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;

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

    // Player references
    public GameObject player; // Reference to the player GameObject
    private Vector3 playerEntryPosition; // Entry position for the player

    // Tutorial Player references
    public GameObject tutPlayer; // Reference to the tutorial player GameObject
    private Vector3 tutPlayerEntryPosition; // Entry position for the tutorial player

    public Vector3? savedPlayerPosition = null; // Nullable to indicate no saved position by default

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
        // Define the list of scenes where only the player should not persist
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
            "Phone Puzzle"
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
            FindMonster();

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
                initialTutPlayer.SetActive(false);
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
                initialPlayer.SetActive(false);
                Debug.Log("Duplicate initial player destroyed to prevent duplication.");
            }
        }
    }

    private void FindMonster()
    {
        if (monster != null)
        {
            Debug.Log("Monster already exists. Skipping finding a new one.");
            return;
        }

        monster = GameObject.FindWithTag("Monster");
        if (monster != null)
        {
            MeshRenderer renderer = monster.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
                Debug.Log("Monster GameObject found and made invisible in GameManager.");
            }

            DontDestroyOnLoad(monster);
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
           
/*
            if (playerBlood == 0)
            {
                ActivatePanel();
            }
*/
            Debug.Log("Player has no more blood left!");
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

                //$"OpenEye{playerBlood + 1}"
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
 /*
    private void DeactivateGamePanel()
    {
        if (gamePanel != null)
        {
            gamePanel.SetActive(false); // Deactivate the game panel
            Debug.Log("Game panel deactivated!");
        }

    }
*/


    private void PersistAcrossScenes(params GameObject[] panels)
    {
        foreach (GameObject panel in panels)
        {
            if (panel != null) DontDestroyOnLoad(panel);
        }
    }

    public void SavePlayerPosition()
    {
        if (player != null)
        {
            savedPlayerPosition = player.transform.position;
            Debug.Log("Player position saved: " + savedPlayerPosition);
        }
        else
        {
            Debug.LogWarning("Player reference is null; unable to save position.");
        }
    }

    public void RestorePlayerPosition()
    {
        if (savedPlayerPosition.HasValue && player != null)
        {
            player.transform.position = savedPlayerPosition.Value;
            Debug.Log("Player position restored: " + savedPlayerPosition);
            savedPlayerPosition = null; // Clear the saved position after restoring
        }
        else
        {
            Debug.LogWarning("No saved position to restore or player reference is null.");
        }
    }

    private bool IsInNonPersistentScene()
    {
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
            "Phone Puzzle"
        };
        return playerNonPersistentScenes.Contains(SceneManager.GetActiveScene().name);
    }
}
