using UnityEngine;
using UnityEngine.SceneManagement;

public class LivingRoomBloodElimination : MonoBehaviour
{
    public float detectionRadius = 2f;
    public GameObject[] bloodObjects;
    private int bloodCount = 0;
    private TutCharacterSound tutCharacterSound; // Reference to TutCharacterSound

    void Start()
    {
        // Initialize the TutCharacterSound reference
        tutCharacterSound = FindObjectOfType<TutCharacterSound>();
        if (tutCharacterSound == null)
        {
            Debug.LogError("TutCharacterSound script not found on any GameObject in the scene.");
        }

        // Check if the living room is already clean
        if (GameManager.instance.isLivingRoomClean)
        {
            foreach (GameObject blood in bloodObjects)
            {
                Destroy(blood);
            }
            Debug.Log("Living room is already clean.");
            return;
        }

        // Find all blood objects with the "Blood" tag
        bloodObjects = GameObject.FindGameObjectsWithTag("Blood");
    }

    void Update()
    {
        // Check if the cleaning kit is active in the GameManager
        if (GameManager.instance.isCleaningKit)
        {
            foreach (GameObject blood in bloodObjects)
            {
                if (blood != null)
                {
                    // Use Vector2.Distance for 2D distance checking
                    float distanceToBlood = Vector2.Distance(transform.position, blood.transform.position);

                    if (distanceToBlood <= detectionRadius && Input.GetKeyDown(KeyCode.Q))
                    {
                        Destroy(blood);
                        bloodCount++;
                        Debug.Log("Blood eliminated in the living room. Total eliminated: " + bloodCount);

                        // Play cleaning blood sound
                        if (tutCharacterSound != null)
                        {
                            tutCharacterSound.PlayCleaningBloodSound();
                        }

                        // Check if all blood spots are cleaned
                        if (bloodCount == bloodObjects.Length)
                        {
                            GameManager.instance.isLivingRoomClean = true;
                            TransitionManager.instance.ChangeScene("TutLRoomCScene");
                        }
                    }
                }
            }
        }
    }
}