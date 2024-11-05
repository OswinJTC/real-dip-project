using UnityEngine;

public class AmbientSoundTrigger : MonoBehaviour
{
    private void Start()
    {
        // Check if the AudioManager instance exists, then play ambient sound
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayAmbientSound();
        }
        else
        {
            Debug.LogWarning("AudioManager instance not found.");
        }
    }
}