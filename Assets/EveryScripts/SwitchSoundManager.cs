using UnityEngine;

public class SwitchSoundManager : MonoBehaviour
{
    [Header("Switch Sounds")]
    public AudioClip[] switchSounds; // Array of switch sounds
    [SerializeField] private float switchVolume = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Method to play a random switch sound
    public void PlayRandomSwitchSound()
    {
        if (switchSounds.Length > 0)
        {
            // Select a random sound from the array
            AudioClip randomClip = switchSounds[Random.Range(0, switchSounds.Length)];
            audioSource.volume = switchVolume;
            audioSource.PlayOneShot(randomClip);
        }
        else
        {
            Debug.LogWarning("No switch sounds assigned in SwitchSoundManager.");
        }
    }
}