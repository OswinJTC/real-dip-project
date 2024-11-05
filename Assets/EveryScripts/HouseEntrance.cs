using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HouseEntrance : MonoBehaviour
{
    public Transform player;                      // Reference to the player's transform
    public string nextScene = "TutLRoomDScene";   // Scene to load when entering
    private CharacterSounds characterSounds;      // Reference to CharacterSounds on the player
    private bool isTransitioning = false;         // Flag to prevent multiple transitions
    private bool isPlayerNear = false;            // Flag to check if the player is near the entrance

    private void Start()
{
    DontDestroyOnLoad(this);  // Only persist the script itself, not the GameObject

    // If player is not assigned in the Inspector, try finding it in the scene
    if (player == null)
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene! Make sure there is a GameObject tagged as 'Player'.");
            return; // Exit if player is not found
        }
    }

    // Initialize the character sound component
    characterSounds = player.GetComponent<CharacterSounds>();
    if (characterSounds == null)
    {
        Debug.LogError("CharacterSounds component not found on the Player GameObject.");
    }
}
    

    private void Update()
    {
        // Only allow "E" press if the player is nearby and we're not transitioning
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isTransitioning)
        {
            Debug.Log("E pressed. Player entering the house...");

            // Start fading out the ambient sound
            AudioManager.instance.DimAmbientSound(0.3f, 1.5f); // Dim to 30% volume over 1.5 seconds

            // Start coroutine to play sound and handle scene transition
            StartCoroutine(EnterHouseAfterSound());
        }
    }

    private IEnumerator EnterHouseAfterSound()  
    {
    isTransitioning = true; // Prevent re-triggering while transitioning

    // Play the door open-and-close sound through CharacterSounds if it's valid
    if (characterSounds != null && characterSounds.doorOpenClip != null)
    {
        characterSounds.PlayDoorOpenSound();
    }

    // Begin the transition right after starting the sound
    Vector3 entryPosition = GetEntryPosition(nextScene);
    GameManager.instance.SetPlayerEntryPosition(entryPosition);

    if (TransitionManager.instance != null)
    {
        TransitionManager.instance.ChangeScene(nextScene);
    }
    else
    {
        SceneManager.LoadScene(nextScene);
    }

    // Yield once to satisfy IEnumerator requirement without delaying the scene transition
    yield return null;
    }

    private Vector3 GetEntryPosition(string sceneToLoad)
    {
        // Define custom entry position based on scene name
        if (sceneToLoad == "TutLRoomDScene")
        {
            return new Vector3(5.23f, 5.17f, 11.34f);
        }
        return new Vector3(0f, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the entrance.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player has left the entrance.");
        }
    }
}