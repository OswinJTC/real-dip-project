using UnityEngine;
using UnityEngine.SceneManagement;  // Include this to manage scene loading

public class MCQTrigger : MonoBehaviour
{
    public GameObject mcqPanel;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mcqPanel.SetActive(true);
            Time.timeScale = 0;  // Pause the game
        }
    }

    // Call this function when the player presses the button after completing the MCQ
    public void LoadNewScene()
    {
        Time.timeScale = 1;  // Resume the game
        SceneManager.LoadScene("Warehouse");  // Replace "warehouse" with the name of the new scene
    }
}
