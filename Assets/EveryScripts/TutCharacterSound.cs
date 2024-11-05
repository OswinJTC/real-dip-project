using UnityEngine;

public class TutCharacterSound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource humAudioSource; // Separate AudioSource for hum

    [Header("Footstep Sounds")]
    [SerializeField] private AudioClip[] footstepClips; // Footstep sounds for walking
    [SerializeField] private float footstepVolume = 0.5f;

    [Header("Interaction Sounds")]
    [SerializeField] public AudioClip electricalBoxOpenClip; // Sound for opening the electrical box
    [SerializeField] private float interactionVolume = 0.7f;

    [Header("Hum Sound")]
    [SerializeField] public AudioClip electricalBoxHumClip; // Sound for electrical box hum
    [SerializeField] private float humVolume = 0.7f;

    [Header("Cleaning Sound")]
    [SerializeField] private AudioClip cleaningBloodClip; // Sound for cleaning blood
    [SerializeField] private float cleaningVolume = 0.7f;

    [Header("Door Open Sound")]
    [SerializeField] private AudioClip doorOpenClip; // Sound for opening the tutorial door
    [SerializeField] private float doorVolume = 0.7f;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        humAudioSource = gameObject.AddComponent<AudioSource>(); // Add a second AudioSource for the hum
        audioSource.playOnAwake = false;
        humAudioSource.playOnAwake = false;
    }

    // Method for playing footsteps, triggered by animation event
    public void PlayFootstepSound()
    {
        if (footstepClips != null && footstepClips.Length > 0)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.volume = footstepVolume;
            audioSource.pitch = Random.Range(0.9f, 1.1f); // Slight variation in pitch
            audioSource.PlayOneShot(clip);
        }
    }

    // Method to play the electrical box sound when interacting
    public void PlayElectricalBoxOpenSound()
    {
        if (electricalBoxOpenClip != null)
        {
            audioSource.volume = interactionVolume;
            audioSource.PlayOneShot(electricalBoxOpenClip);
        }
        else
        {
            Debug.LogWarning("Electrical box open sound clip is missing.");
        }
    }

    // Method to play the hum sound when puzzle is solved
    public void PlayElectricalBoxHum()
    {
        if (electricalBoxHumClip != null)
        {
            humAudioSource.clip = electricalBoxHumClip;
            humAudioSource.loop = true; // Set to loop for continuous hum
            humAudioSource.volume = humVolume;
            humAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Electrical box hum sound clip is missing.");
        }
    }

    // Stop the hum sound
    public void StopElectricalBoxHum()
    {
        if (humAudioSource.isPlaying && humAudioSource.clip == electricalBoxHumClip)
        {
            humAudioSource.Stop();
        }
    }

    // Method to play the cleaning blood sound
    public void PlayCleaningBloodSound()
    {
        if (cleaningBloodClip != null)
        {
            audioSource.volume = cleaningVolume;
            audioSource.PlayOneShot(cleaningBloodClip);
        }
        else
        {
            Debug.LogWarning("Cleaning blood sound clip is missing.");
        }
    }

    // Method to play the door open sound
    public void PlayDoorOpenSound()
    {
        if (doorOpenClip != null)
        {
            audioSource.volume = doorVolume;
            audioSource.PlayOneShot(doorOpenClip);
        }
        else
        {
            Debug.LogWarning("Door open sound clip is missing.");
        }
    }
}