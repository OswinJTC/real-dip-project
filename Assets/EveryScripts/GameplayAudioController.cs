using UnityEngine;

public class GameplayAudioController : MonoBehaviour
{
    private void Start()
    {
        // Ensure AudioManager instance exists
        if (AudioManager.instance != null)
        {
            // Stop background music if it's playing
            AudioManager.instance.StopBackgroundMusic();

            // Play only the ambient sound with a fade-in effect
            AudioManager.instance.PlayAmbientSound(5.0f); // 2.0 seconds fade-in duration
        }
        else
        {
            Debug.LogWarning("AudioManager instance not found.");
        }
    }
}