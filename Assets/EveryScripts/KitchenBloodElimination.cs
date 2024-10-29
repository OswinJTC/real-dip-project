using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenBloodElimination : MonoBehaviour
{
    public float detectionRadius = 2f;
    public GameObject[] bloodObjects;
    private int bloodCount = 0;

    void Start()
    {
        if (GameManager.instance.isKitchenClean)
        {
            foreach (GameObject blood in bloodObjects)
            {
                Destroy(blood);
            }
            Debug.Log("Kitchen is already clean.");
            return;
        }

        bloodObjects = GameObject.FindGameObjectsWithTag("Blood");
    }

    void Update()
    {
        // Check if the cleaning kit is enabled instead of item activation
        if (GameManager.instance.isCleaningKit)
        {
            foreach (GameObject blood in bloodObjects)
            {
                if (blood != null)
                {
                    float distanceToBlood = Vector3.Distance(transform.position, blood.transform.position);

                    if (distanceToBlood <= detectionRadius && Input.GetKeyDown(KeyCode.Q))
                    {
                        Destroy(blood);
                        bloodCount++;
                        Debug.Log("Blood eliminated in the kitchen. Total eliminated: " + bloodCount);

                        if (bloodCount == bloodObjects.Length)
                        {
                            GameManager.instance.isKitchenClean = true;
                            TransitionManager.instance.ChangeScene("TutKitchenCScene");
                        }
                    }
                }
            }
        }
    }
}
