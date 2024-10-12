using UnityEngine;
using UnityEngine.UI;

public class FlickeringImage : MonoBehaviour
{
    public Image image; // Assign your Image component here.
    public float flickerSpeed = 5f; // How fast it flickers.
    public float minAlpha = 0.3f; // Minimum alpha (opacity) value.
    public float maxAlpha = 1f; // Maximum alpha value.

    private float targetAlpha;
    private Color currentColor;

    void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>(); // Automatically grab Image component if not assigned.
        }

        // Set the initial color and alpha values
        currentColor = image.color;
        targetAlpha = maxAlpha;
    }

    void Update()
    {
        // Check if the current alpha is close enough to the targetAlpha
        if (Mathf.Abs(currentColor.a - targetAlpha) < 0.01f)
        {
            // Set a new random target alpha when the current alpha reaches the target
            targetAlpha = Random.Range(minAlpha, maxAlpha);
        }

        // Smoothly change the alpha value
        currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, flickerSpeed * Time.deltaTime);

        // Update the image color with the new alpha
        image.color = currentColor;
    }
}
