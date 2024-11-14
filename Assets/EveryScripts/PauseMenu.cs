using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Reference to the pause panel
    private bool isPaused = false;

    void Update()
    {
        // Check if the Esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            
                Pause();
            
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true); // Show the pause panel
        
        isPaused = true; // Set paused state
    }

    public void Resume()
    {
        pausePanel.SetActive(false); // Hide the pause panel
        
        isPaused = false; // Reset paused state
    }

    public void SaveGame()
    {
        // Implement your save game logic here
        Debug.Log("Game Saved!");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
