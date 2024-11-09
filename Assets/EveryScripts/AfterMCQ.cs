using UnityEngine;
using UnityEngine.SceneManagement;

public class AfdterMCQ : MonoBehaviour
{
    // Specify the scene name directly in the script
    private string sceneName = "Main Menu"; 

    // Method to be called when the button is pressed
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
