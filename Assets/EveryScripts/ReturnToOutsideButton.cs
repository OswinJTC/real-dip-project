using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToOutsideButton : MonoBehaviour
{
    private void Start()
    {
        // Ensure this button calls ReturnToOutside when clicked
        GetComponent<Button>().onClick.AddListener(ReturnToOutside);
    }

    private void ReturnToOutside()
    {
        Debug.Log("Returning to outsideTerrain.");

        // Update inventory to activate bread item
        GameManager.instance.isBreadActive = true;
        GameManager.instance.UpdateInventoryUI();

        // Switch scene
        if (TransitionManager.instance != null)
        {
            TransitionManager.instance.ChangeScene("outsideTerrain");
            UIManager.instance.ShowPrompt("Bread collected...leaving the backery...", 2f);
        }
        else
        {
            SceneManager.LoadScene("outsideTerrain");
            UIManager.instance.ShowPrompt("Bread collected...leaving the backery...", 2f);
        }
    }
}
