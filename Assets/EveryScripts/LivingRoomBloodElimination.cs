using UnityEngine;
using UnityEngine.SceneManagement;

public class LivingRoomBloodElimination : MonoBehaviour
{
    public float detectionRadius = 2f;
    public GameObject[] bloodObjects;
    private ItemActivation itemActivation;
    private int bloodCount = 0;

    void Start()
    {
        if (GameManager.instance.isLivingRoomClean)
        {
            foreach (GameObject blood in bloodObjects)
            {
                Destroy(blood);
            }
            Debug.Log("Living room is already clean.");
            return;
        }

        bloodObjects = GameObject.FindGameObjectsWithTag("Blood");
        itemActivation = FindObjectOfType<ItemActivation>();
    }

    void Update()
    {
        if (itemActivation.IsItemEquipped())
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
                        Debug.Log("Blood eliminated in the living room. Total eliminated: " + bloodCount);

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
