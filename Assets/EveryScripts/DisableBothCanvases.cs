using UnityEngine;

public class DisableAllCanvases : MonoBehaviour
{
    private GameObject persistentCanvas1;
    private GameObject persistentCanvas2;
    private GameObject persistentCanvas3;
    private GameObject persistentCanvas4;

    private void Start()
    {
        // Find and disable the first Canvas with the "PersistentCanvas" tag
        persistentCanvas1 = GameObject.FindGameObjectWithTag("PersistentCanvas");
        if (persistentCanvas1 != null)
        {
            persistentCanvas1.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Canvas with tag 'PersistentCanvas' not found.");
        }

        // Find and disable the second Canvas with the "PersistentGameOverCanvas" tag
        persistentCanvas2 = GameObject.FindGameObjectWithTag("PersistentGameOverCanvas");
        if (persistentCanvas2 != null)
        {
            persistentCanvas2.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Canvas with tag 'PersistentGameOverCanvas' not found.");
        }

        // Find and disable the third Canvas with the "PersistentCanvas3" tag
        persistentCanvas3 = GameObject.FindGameObjectWithTag("PersistentCanvas3");
        if (persistentCanvas3 != null)
        {
            persistentCanvas3.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Canvas with tag 'PersistentCanvas3' not found.");
        }

        // Find and disable the fourth Canvas with the "PersistentCanvas4" tag
        persistentCanvas4 = GameObject.FindGameObjectWithTag("Player");
        if (persistentCanvas4 != null)
        {
            persistentCanvas4.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Canvas with tag 'PersistentCanvas4' not found.");
        }
    }

    private void OnDestroy()
    {
        // Re-enable all Canvases when leaving the scene
        if (persistentCanvas1 != null)
        {
            persistentCanvas1.SetActive(true);
        }

        if (persistentCanvas2 != null)
        {
            persistentCanvas2.SetActive(true);
        }

        if (persistentCanvas3 != null)
        {
            persistentCanvas3.SetActive(true);
        }

        if (persistentCanvas4 != null)
        {
            persistentCanvas4.SetActive(true);
        }
    }
}
