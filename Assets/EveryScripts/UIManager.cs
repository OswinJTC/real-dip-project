using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private Text promptText;

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
                    promptText.color = Color.white;

                    // Position the text lower on the screen
                    RectTransform rectTransform = promptText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(1000, 150);  
                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);

                    // Adjust this value to move the text lower
                    rectTransform.anchoredPosition = new Vector2(0, -450); 
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

        if (promptText != null)
        {
            StartCoroutine(DisplayPrompt(message, duration));
        }
    }

    private IEnumerator DisplayPrompt(string message, float duration)
    {
        promptText.text = message;
        promptText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        promptText.gameObject.SetActive(false);
    }
}
