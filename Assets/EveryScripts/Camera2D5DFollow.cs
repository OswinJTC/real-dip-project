using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera2D5DFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;

    void Start()
    {
        // Find the player if the target is not set
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        // Adjust distance and height based on the initial scene
        AdjustCameraDistanceAndHeight(SceneManager.GetActiveScene().name);

        // Subscribe to scene changes to adjust distance and height
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void LateUpdate()
    {
        if (target != null && gameObject.activeSelf) // Ensure the camera is active
        {
            Vector3 desiredPosition = new Vector3(
                target.position.x, 
                target.position.y + height, 
                target.position.z - distance
            );
            transform.position = desiredPosition;
            transform.rotation = Quaternion.Euler(30f, 0f, 0f);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AdjustCameraDistanceAndHeight(scene.name);
    }

    void AdjustCameraDistanceAndHeight(string sceneName)
{
    if (sceneName == "outsideTerrain")
    {
        // Set distance and height for the outsideTerrain scene
        distance = 2.5f;
        height = 2f;
        gameObject.SetActive(true); // Ensure the camera is active
        Debug.Log("Camera distance and height adjusted for outsideTerrain.");
    }
    else if (sceneName == "BBHideBRoom" || sceneName == "BBHideLRoom" || sceneName == "Balloon Puzzle" || sceneName == "BakeryScene")
    {
        // Disable the camera in the hiding scenes and Balloon Puzzle
        gameObject.SetActive(false);
        Debug.Log("Camera disabled for BBHideBRoom, BBHideLRoom, or Balloon Puzzle.");
    }
    else
    {
        // Set distance and height for any other scene
        distance = 10f;
        height = 8f;
        gameObject.SetActive(true); // Ensure the camera is active
        Debug.Log("Camera distance and height adjusted for other scenes.");
    }
}


    void OnDestroy()
    {
        // Unsubscribe from the scene load event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
