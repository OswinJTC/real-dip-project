using UnityEngine;

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
}
