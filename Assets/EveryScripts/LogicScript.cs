using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public Camera myCamera;
    private GameObject[] LogicTag;
    private ElectricBoxLightControl electricBoxLightControl;

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
            UIManager.instance.ShowPrompt("Well, ain’t this a lovely sight... Seen worse, but not by much. Time to roll up my sleeves.", 10f);
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
                    SceneManager.LoadScene("TutLRoomDScene");
                    UIManager.instance.ShowPrompt("Well, ain’t this a lovely sight... Seen worse, but not by much. Time to roll up my sleeves.", 10f);
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