using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Footstep Sound")]
    [SerializeField] private AudioClip[] footstepClips; // Array of footstep sounds
    [SerializeField] private float footstepVolume = 0.5f;

    [Header("Interaction Sounds")]
    [SerializeField] public AudioClip doorOpenClip;
    [SerializeField] private float interactionVolume = 0.7f;

    private void Awake()
    {
        // Get the AudioSource component attached to the character
        audioSource = GetComponent<AudioSource>();
    }

    // This method will be called by the animation event
    public void PlayFootstepSound()
    {
        if (audioSource != null && footstepClips.Length > 0)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.pitch = Random.Range(-3.0f, 3.0f); // Slightly vary the pitch
            audioSource.volume = footstepVolume;
            // Log the pitch to the Console
            // Debug.Log("Playing footstep with pitch: " + audioSource.pitch);
            audioSource.PlayOneShot(clip);
            audioSource.pitch = 1.0f; // Reset pitch after playing
        }
    }   

    // Method to play the door open sound
    public void PlayDoorOpenSound()
    {
        if (audioSource != null && doorOpenClip != null)
        {
            audioSource.volume = interactionVolume;
            audioSource.PlayOneShot(doorOpenClip);
        }
    }
}
