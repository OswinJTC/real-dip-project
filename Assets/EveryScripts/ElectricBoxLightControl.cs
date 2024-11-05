using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ElectricBoxLightControl : MonoBehaviour
{
    public Light darkDirectionalLight;
    public Light lightDirectionalLight;
    public Light playerSpotlight;
    public Transform player;
    public float detectionRadius = 20f;
    public float spotlightHeight = 5f;

    private bool isLightOn = false;
    private TutCharacterSound tutCharacterSound;

    void Start()
    {
        DontDestroyOnLoad(this);  // Only persist the script itself, not the GameObject

        // Initialize the character sound component
        if (player != null)
        {
            tutCharacterSound = player.GetComponent<TutCharacterSound>();
        }

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
                    Debug.Log("E key pressed. Playing electrical box sound and transitioning...");
                    StartCoroutine(OpenElectricalBoxAfterSound());
                }
            }
        }
    }

    private IEnumerator OpenElectricalBoxAfterSound()
    {
        // Play the electrical box open sound if available
        if (tutCharacterSound != null && tutCharacterSound.electricalBoxOpenClip != null)
        {
            tutCharacterSound.PlayElectricalBoxOpenSound();
        }

        yield return new WaitForSeconds(1.5f); // Wait for a moment to let sound play, adjust as needed

        GoToPuzzleScene();
        TurnOnBrightLights();
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

        // Play the electrical box hum sound on loop if lights are turned on
        if (tutCharacterSound != null)
        {
            tutCharacterSound.PlayElectricalBoxHum();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}