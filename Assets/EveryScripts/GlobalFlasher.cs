using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalFlasher : MonoBehaviour
{
    public static GlobalFlasher instance; // Singleton instance
    public Image flashPanel; // Reference to the UI Image for flashing

    private bool isFlashing = false; // Prevent multiple concurrent flashes

    private void Awake()
    {
        // Singleton pattern to ensure one instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GlobalFlasher instance created.");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate GlobalFlasher destroyed.");
        }
    }

    public void FlashWarning()
    {
        if (!isFlashing && flashPanel != null)
        {
            StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        isFlashing = true;

        // Flash effect logic: Change the alpha of the flashPanel's color
        for (int i = 0; i < 3; i++) // Flash 3 times
        {
            flashPanel.color = new Color(1f, 0f, 0f, 0.5f); // Red color with 50% opacity
            yield return new WaitForSeconds(0.1f);
            flashPanel.color = new Color(1f, 0f, 0f, 0f); // Transparent
            yield return new WaitForSeconds(0.1f);
        }

        isFlashing = false;
    }
}
