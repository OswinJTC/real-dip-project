using UnityEngine;

public class DisablePersistentCanvas : MonoBehaviour
{
    private GameObject persistentCanvas;

    private void Start()
    {
        // Find the Canvas with the "PersistentCanvas" tag
        persistentCanvas = GameObject.FindGameObjectWithTag("PersistentCanvas");

        if (persistentCanvas != null)
        {
            // Disable the Canvas component to hide the UI
            persistentCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Persistent Canvas with tag 'PersistentCanvas' not found in the scene.");
        }
    }

    private void OnDestroy()
    {
        // Re-enable the Canvas when leaving the scene
        if (persistentCanvas != null)
        {
            persistentCanvas.SetActive(true);
        }
    }
}
