using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("-------- Audio Sources --------")]
    [SerializeField] private AudioSource musicSource; // For background music
    [SerializeField] private AudioSource ambientSource; // For ambient sounds

    [Header("-------- Audio Clips --------")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip ambientSound;

    private Coroutine fadeInCoroutine; // Track the active FadeIn coroutine
    private float currentAmbientVolume = 1.0f; // Default volume level

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make the AudioManager persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate AudioManager instances
            return;
        }
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music source or background music clip is missing.");
        }
    }

    public void PlayAmbientSound(float fadeDuration = 1.0f)
    {
        if (ambientSource != null && ambientSound != null)
        {
            if (!ambientSource.isPlaying)
            {
                ambientSource.clip = ambientSound;
                ambientSource.loop = true;
                ambientSource.volume = 0; // Start at 0 volume for fade-in
                ambientSource.Play();

                // Start FadeIn and track the coroutine
                fadeInCoroutine = StartCoroutine(FadeIn(ambientSource, fadeDuration));
            }
        }
        else
        {
            Debug.LogWarning("Ambient source or ambient clip is missing.");
        }
    }

    public void StopBackgroundMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void StopAmbientSound()
    {
        if (ambientSource != null && ambientSource.isPlaying)
        {
            ambientSource.Stop();
        }
    }

    public void DimAmbientSound(float targetVolume = 0.3f, float fadeDuration = 0.2f)
    {
        if (ambientSource != null && ambientSource.isPlaying)
        {
            // Stop any active FadeIn coroutine
            if (fadeInCoroutine != null)
            {
                StopCoroutine(fadeInCoroutine);
                fadeInCoroutine = null;
            }

            // Update the saved volume level and start dimming
            currentAmbientVolume = targetVolume;
            StartCoroutine(FadeToVolume(ambientSource, targetVolume, fadeDuration));
        }
        else
        {
            Debug.LogWarning("Ambient source is missing or not currently playing.");
        }
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float targetVolume = 1.0f; // Max volume
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume; // Ensure it reaches the target volume at the end
        fadeInCoroutine = null; // Clear the reference when done
    }

    private IEnumerator FadeToVolume(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume; // Ensure it reaches the target volume at the end
    }
}