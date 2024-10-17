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
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true); // Show the pause panel
        Time.timeScale = 0f; // Stop the game
        isPaused = true; // Set paused state
    }

    public void Resume()
    {
        pausePanel.SetActive(false); // Hide the pause panel
        Time.timeScale = 1f; // Resume the game
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
