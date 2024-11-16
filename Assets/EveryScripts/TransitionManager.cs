using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;   // Singleton pattern to persist across scenes
    public Image fadeImage;                     // Reference to the fade image for transitions
    public float fadeDuration = 1f;             // Duration of the fade

    private GameObject transitionCanvas;        // Reference to the TransitionCanvas

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);      // Ensure this object persists across scenes
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to sceneLoaded event
        }
        else
        {
            Destroy(gameObject);                // Prevent duplicate instances
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (transitionCanvas == null)
        {
            transitionCanvas = GameObject.Find("TransitionCanvas"); // Find the TransitionCanvas in the scene
        }

        if (scene.name == "MCQ")
        {
            if (transitionCanvas != null)
            {
                transitionCanvas.SetActive(false); // Deactivate the TransitionCanvas
                Debug.Log("TransitionCanvas deactivated in scene: " + scene.name);
            }
            else
            {
                Debug.LogWarning("TransitionCanvas not found in scene: " + scene.name);
            }
        }
        else
        {
            if (transitionCanvas != null)
            {
                transitionCanvas.SetActive(true); // Reactivate the TransitionCanvas
                Debug.Log("TransitionCanvas reactivated in scene: " + scene.name);
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeOutAndChangeScene(sceneName));
    }

    private IEnumerator FadeOutAndChangeScene(string sceneName)
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("Fade image not assigned.");
            yield break;
        }

        // Start with the image being fully transparent (alpha = 0)
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);

        // Fade out: gradually increase the alpha to 1
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }

        // After fade-out, load the next scene
        SceneManager.LoadScene(sceneName);

        // Optionally, fade in after the scene loads
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        // Start with the image being fully opaque (alpha = 1)
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);

        // Fade in: gradually decrease the alpha to 0
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }

        // Fully transparent after fade-in
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
    }
}
