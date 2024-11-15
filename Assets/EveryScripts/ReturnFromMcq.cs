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
        SceneManager.LoadScene("BedroomScene");
    }
}
