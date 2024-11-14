using UnityEngine;
using UnityEngine.SceneManagement;

public class ColourScript : MonoBehaviour
{
    public Camera myCamera;
    private GameObject[] SphereTag;

    void Start()
    {
        SphereTag = GameObject.FindGameObjectsWithTag("Sphere");

        for (int i = 0; i < SphereTag.Length; i++)
        {
            if (i % 2 == 0)
            {
                SphereTag[i].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                SphereTag[i].GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }

        if (Input.GetKeyDown(KeyCode.L)) // Press "L" to skip the game
        {
            SkipToBBLivingroom();
        }

        CheckRed();
        CheckBlue();
    }

    void HandleMouseClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray myRay = myCamera.ScreenPointToRay(mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(myRay, out raycastHit))
        {
            for (int i = 0; i < SphereTag.Length; i++)
            {
                if (Vector3.Distance(raycastHit.transform.position, SphereTag[i].transform.position) == 5)
                {
                    if (SphereTag[i].GetComponent<Renderer>().material.color == Color.blue)
                    {
                        SphereTag[i].GetComponent<Renderer>().material.color = Color.red;
                    }
                    else
                    {
                        SphereTag[i].GetComponent<Renderer>().material.color = Color.blue;
                    }
                }
            }

            // Toggle clicked sphere color
            Color color = raycastHit.transform.gameObject.GetComponent<Renderer>().material.color;
            raycastHit.transform.GetComponent<MeshRenderer>().material.color = color == Color.blue ? Color.red : Color.blue;
        }
    }

    void CheckRed()
    {
        foreach (var sphere in SphereTag)
        {
            if (sphere.GetComponent<Renderer>().material.color != Color.red)
                return;
        }

        Debug.Log("YATTA RED");

        if (GameManager.instance != null)
        {
            GameManager.instance.isBalloonActive = true;
            GameManager.instance.UpdateInventoryUI(); // Update the inventory UI to reflect the change
            UIManager.instance.ShowPrompt("Balloon collected...all the best...", 3f);
        }
        SceneManager.LoadScene("BBLivingroomScene");
    }

    void CheckBlue()
    {
        foreach (var sphere in SphereTag)
        {
            if (sphere.GetComponent<Renderer>().material.color != Color.blue)
                return;
        }

        Debug.Log("YATTA BLUE");
        
        if (GameManager.instance != null)
        {
            GameManager.instance.isBalloonActive = true;
            GameManager.instance.UpdateInventoryUI(); // Update the inventory UI to reflect the change
            UIManager.instance.ShowPrompt("Balloon collected...all the best...", 3f);
        }
        SceneManager.LoadScene("BBLivingroomScene");
    }

    void SkipToBBLivingroom()
    {
        Debug.Log("Skipping puzzle and loading BBLivingroomScene.");

        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Save the player's current position
        GameManager.instance.SavePlayerPosition(currentSceneName);

        // Load the next scene
        SceneManager.LoadScene("BBLivingroomScene");

        // When entering "BBLivingroomScene," restore the position
        GameManager.instance.RestorePlayerPosition("BBLivingroomScene");
    }
}
