using System.Collections;
using System.Collections.Generic;
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
    }

    void CheckBlue()
    {
        foreach (var sphere in SphereTag)
        {
            if (sphere.GetComponent<Renderer>().material.color != Color.blue)
                return;
        }

        Debug.Log("YATTA BLUE");
    }

    void SkipToBBLivingroom()
    {
        Debug.Log("Skipping puzzle and loading BBLivingroomScene.");

        // Save the player's current position
        GameManager.instance.SavePlayerPosition();

        // Load the next scene
        SceneManager.LoadScene("BBLivingroomScene");

        // When entering "BBLivingroomScene," restore the position
        GameManager.instance.RestorePlayerPosition();
    }
}
