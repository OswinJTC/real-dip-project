using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnFromMcq: MonoBehaviour
{
    private void Start()
    {
        // Ensure this button calls ReturnToOutside when clicked
        GetComponent<Button>().onClick.AddListener(ReturnToOutside);
    }

    private void ReturnToOutside()
    {
        Debug.Log("Returning to game.");

        // Switch scene
        if (TransitionManager.instance != null)
        {
            TransitionManager.instance.ChangeScene("BedroomScene");
        }
        else
        {
            SceneManager.LoadScene("BedroomScene");
        }
    }
}
