using UnityEngine;
using UnityEngine.UI;  // Required for UI elements
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance
    public Text timerText;               // Reference to the UI Text to show the timer
    public GameObject hourglassUIIcon;   // Persistent reference to the hourglass icon in the UI

    private float timer = 0f;            // Tracks the elapsed time
    private List<string> inventory = new List<string>();  // Simple inventory to track collected items

    // Global variables to track if the blood has been eliminated in each room
    public bool isLivingRoomClean = false;
    public bool isRoomClean = false;
    public bool isKitchenClean = false;
    public bool isStudyRoomClean = false;

    // Global variable to track if the lights are turned on
    public bool isLightOn = false;       // New variable to track whether the lights are turned on

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy any duplicate GameManager
        }
    }

    private void Start()
    {
        if (hourglassUIIcon != null)
        {
            hourglassUIIcon.SetActive(false);  // Initially hide the hourglass icon
        }
    }

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Convert the timer to minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        // Update the UI Text with the formatted time (mm:ss)
        if (timerText != null)
        {
            timerText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
        }
    }

    public void AddToInventory(string itemName)
    {
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName);
            Debug.Log(itemName + " added to inventory.");
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
}
