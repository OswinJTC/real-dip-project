using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public string nextScene; // The scene to load when this door is opened
    private bool isPlayerNear = false;
    private GameObject player;
    private static string lastEnteredDoor = ""; // Track the last entered door

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E near the door: " + gameObject.name);
            lastEnteredDoor = gameObject.name; // Store the name of the door being entered
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isPlayerNear = true;
            Debug.Log("Player is near door " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null;
            Debug.Log("Player left door " + gameObject.name);
        }
    }

    public static string GetLastEnteredDoor()
    {
        return lastEnteredDoor;
    }
}
