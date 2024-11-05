using UnityEngine;

public class MainMenuAudioController : MonoBehaviour
{
    private void Start()
    {
        // Ensure AudioManager instance exists
        if (AudioManager.instance != null)
        {
            // Stop any ambient sound that might be playing
            AudioManager.instance.StopAmbientSound();

            // Play background music for the main menu
            AudioManager.instance.PlayBackgroundMusic();
        }
        else
        {
            Debug.LogWarning("AudioManager instance not found.");
        }
    }
}