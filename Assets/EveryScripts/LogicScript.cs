using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public Camera myCamera;
    private GameObject[] LogicTag;
    private ElectricBoxLightControl electricBoxLightControl;

    private SwitchSoundManager switchSoundManager; // Reference to SwitchSoundManager

    void Start()
    {
        LogicTag = GameObject.FindGameObjectsWithTag("Logic");

        for (int i = 0; i < LogicTag.Length; i++)
        {
            LogicTag[i].GetComponent<Renderer>().material.color = Color.red;
        }

        electricBoxLightControl = FindObjectOfType<ElectricBoxLightControl>();

        if (electricBoxLightControl == null)
        {
            Debug.LogError("ElectricBoxLightControl not found!");
        }
        // Find and assign the SwitchSoundManager component
        switchSoundManager = FindObjectOfType<SwitchSoundManager>();
        if (switchSoundManager == null)
    {
        Debug.LogError("SwitchSoundManager not found! Ensure it’s in the scene.");
    }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandlePuzzleClick();
        }

        if (Input.GetKeyDown(KeyCode.L))  // Press "L" to skip puzzle
        {
            Debug.Log("Puzzle skipped by pressing L.");
            SolvePuzzle();
            TurnOnLights();
            ReturnToRoom();  // Transition back to the room after skipping the puzzle
        }

        CheckGreen();
    }

    void HandlePuzzleClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray myRay = myCamera.ScreenPointToRay(mousePosition);
        RaycastHit raycastHit;

        bool HitSomething = Physics.Raycast(myRay, out raycastHit);

        if (HitSomething)
        {
            if ((raycastHit.transform.gameObject.name != "Switch F") && (raycastHit.transform.gameObject.tag == "Logic"))
            {
                Color color = raycastHit.transform.gameObject.GetComponent<Renderer>().material.color;
                if (color == Color.green)
                {
                    raycastHit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                else
                {
                    raycastHit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                // Play random switch sound through the SwitchSoundManager
                if (switchSoundManager != null)
                {
                    switchSoundManager.PlayRandomSwitchSound();
                }
            }
        }
    }

    void SolvePuzzle()
    {
        for (int i = 0; i < LogicTag.Length; i++)
        {
            LogicTag[i].GetComponent<Renderer>().material.color = Color.green;
        }
        Debug.Log("Puzzle solved.");
    }

    void CheckGreen()
    {
        if ((LogicTag[1].GetComponent<Renderer>().material.color != Color.red) &&
            (LogicTag[4].GetComponent<Renderer>().material.color != Color.red))
        {
            if ((LogicTag[0].GetComponent<Renderer>().material.color != Color.green) &&
                (LogicTag[2].GetComponent<Renderer>().material.color != Color.green))
            {
                if (LogicTag[3].GetComponent<Renderer>().material.color != Color.green)
                {
                    LogicTag[5].GetComponent<MeshRenderer>().material.color = Color.green;
                    Debug.Log("You win");
                }
            }
        }
    }

    void TurnOnLights()
    {
        if (electricBoxLightControl != null)
        {
            electricBoxLightControl.TurnOnBrightLights();
        }
        else
        {
            Debug.LogError("ElectricBoxLightControl script not found!");
        }
    }

    void ReturnToRoom()
    {
        SceneManager.LoadScene("TutLRoomDScene");  // Replace with your room scene name
    }
}
