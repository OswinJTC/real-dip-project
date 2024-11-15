using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private Text promptText;
    private Image background;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            FindPromptText();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FindPromptText()
    {
        if (promptText == null)
        {
            promptText = GameObject.Find("PromptText")?.GetComponent<Text>();

            if (promptText == null)
            {
                GameObject canvas = GameObject.Find("PersistentCanvas");

                if (canvas != null)
                {
                    GameObject newPromptText = new GameObject("PromptText");
                    newPromptText.transform.SetParent(canvas.transform);

                    promptText = newPromptText.AddComponent<Text>();
                    promptText.alignment = TextAnchor.MiddleCenter;
                    promptText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                    promptText.fontSize = 50;
                    promptText.color = Color.black; // Set text color to black

                    // Add a background image
                    GameObject backgroundObject = new GameObject("PromptBackground");
                    backgroundObject.transform.SetParent(canvas.transform);

                    background = backgroundObject.AddComponent<Image>();
                    background.color = Color.white; // Set background color to white

                    RectTransform backgroundRect = background.GetComponent<RectTransform>();
                    backgroundRect.sizeDelta = new Vector2(1000, 150);
                    backgroundRect.anchorMin = new Vector2(0.5f, 0.5f);
                    backgroundRect.anchorMax = new Vector2(0.5f, 0.5f);
                    backgroundRect.pivot = new Vector2(0.5f, 0.5f);
                    backgroundRect.anchoredPosition = new Vector2(0, -425);

                    RectTransform rectTransform = promptText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(1000, 150);
                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    rectTransform.anchoredPosition = new Vector2(0, -425);

                    // Ensure the text is on top of the background
                    newPromptText.transform.SetSiblingIndex(backgroundObject.transform.GetSiblingIndex() + 1);
                }
                else
                {
                    Debug.LogError("PersistentCanvas not found in the scene.");
                }
            }
        }
    }

    public void ShowPrompt(string message, float duration)
    {
        if (promptText == null)
        {
            FindPromptText(); // Try to find it again in case of scene reload
        }

        if (promptText != null && background != null)
        {
            StartCoroutine(DisplayPrompt(message, duration));
        }
    }

    private IEnumerator DisplayPrompt(string message, float duration)
    {
        promptText.text = message;
        promptText.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        promptText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }
}
