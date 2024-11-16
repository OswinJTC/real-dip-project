using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipVideo : MonoBehaviour
{
    // Specify the scene name directly in the script
    private string sceneName = "outsideTerrain"; 

    // Method to be called when the button is pressed
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
