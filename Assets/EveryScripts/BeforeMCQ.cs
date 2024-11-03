using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeMCQ : MonoBehaviour
{
    public string mcqSceneName = "MCQ"; // The name of the scene with the MCQ questions

    // Call this function when the Yes button is pressed
    public void LoadScene()
    {
        // Use the TransitionManager to load the MCQ scene with a fade effect, if available
        if (TransitionManager.instance != null)
        {
            TransitionManager.instance.ChangeScene(mcqSceneName);
        }
        else
        {
            Debug.LogWarning("TransitionManager instance not found, loading the scene directly.");
            SceneManager.LoadScene(mcqSceneName); // Fallback to direct scene loading if TransitionManager is not available
        }
    }
}
