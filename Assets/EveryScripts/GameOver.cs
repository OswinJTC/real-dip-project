using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Method to load the main menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Replace "MainMenu" with the actual name of your main menu scene
        Debug.Log("Loading Main Menu...");
    }

    // Method to quit the game
    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit(); // Quits the application

    }
}
