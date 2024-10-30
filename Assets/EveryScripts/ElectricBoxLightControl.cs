using UnityEngine;
using UnityEngine.SceneManagement;

public class ElectricBoxLightControl : MonoBehaviour
{
    public Light darkDirectionalLight;
    public Light lightDirectionalLight;
    public Light playerSpotlight;
    public Transform player;
    public float detectionRadius = 20f;
    public float spotlightHeight = 5f;

    private bool isLightOn = false;

    void Start()
    {
        DontDestroyOnLoad(this);  // Only persist the script itself, not the GameObject

        if (GameManager.instance.isLightOn)
        {
            TurnOnBrightLights();
        }
        else
        {
            if (darkDirectionalLight != null) darkDirectionalLight.enabled = true;
            if (lightDirectionalLight != null) lightDirectionalLight.enabled = false;
            if (playerSpotlight != null) playerSpotlight.enabled = true;
        }
    }

    void Update()
    {
        if (!isLightOn && player != null)
        {
            playerSpotlight.transform.position = player.position + new Vector3(0, spotlightHeight, 0);
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius && !isLightOn)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E key pressed. Going to puzzle scene.");
                    GoToPuzzleScene();
                    TurnOnBrightLights();
                }
            }
        }
    }

    void GoToPuzzleScene()
    {
        SceneManager.LoadScene("LogicPuzzle");
    }

    public void TurnOnBrightLights()
    {
        if (darkDirectionalLight != null) darkDirectionalLight.enabled = false;
        if (playerSpotlight != null) playerSpotlight.enabled = false;
        if (lightDirectionalLight != null) lightDirectionalLight.enabled = true;

        isLightOn = true;
        GameManager.instance.isLightOn = true;

        Debug.Log("Room lights are now on.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
