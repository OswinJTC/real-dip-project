using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomBloodElimination : MonoBehaviour
{
    public float detectionRadius = 2f;
    public GameObject[] bloodObjects;
    private int bloodCount = 0;

    void Start()
    {
        if (GameManager.instance.isRoomClean)
        {
            foreach (GameObject blood in bloodObjects)
            {
                Destroy(blood);
            }
            Debug.Log("Room is already clean.");
            return;
        }

        bloodObjects = GameObject.FindGameObjectsWithTag("Blood");
    }

    void Update()
    {
        // Check if the cleaning kit is available instead of item activation
        if (GameManager.instance.isCleaningKit)
        {
            foreach (GameObject blood in bloodObjects)
            {
                if (blood != null)
                {
                    float distanceToBlood = Vector3.Distance(transform.position, blood.transform.position);

                    if (distanceToBlood <= detectionRadius && Input.GetKeyDown(KeyCode.Q) && GameManager.instance.isCleaningKitInUsed)
                    {
                        Destroy(blood);
                        bloodCount++;
                        Debug.Log("Blood eliminated in the room. Total eliminated: " + bloodCount);

                        if (bloodCount == bloodObjects.Length)
                        {
                            GameManager.instance.isRoomClean = true;
                            TransitionManager.instance.ChangeScene("TutBRoomCScene");
                        }
                    }
                }
            }
        }
    }
}
