using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceScript : MonoBehaviour
{
    
    public GameObject SelectedPiece;
    public int PieceCount=0;
    private GameObject[] PuzzleTag;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PuzzleTag = GameObject.FindGameObjectsWithTag("Puzzl");
       
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.CompareTag("Puzzl"))
            {
                if (!hit.transform.GetComponent<PiecesScript>().InRightPos)
                {
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<PiecesScript>().Selected = true;

                }
                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
           
            SelectedPiece.GetComponent<PiecesScript>().Selected = false;
            SelectedPiece = null;

        }

        if (SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }



        if (IsAllMissionComplete())
        {
            Debug.Log("You win");
        }

       
    }


    private bool IsAllMissionComplete()
    {
        for (int i = 0; i < PuzzleTag.Length; ++i)
        {
            if (PuzzleTag[i].GetComponent<PiecesScript>().InRightPos == false)
            {
                return false;
            }
        }

        return true;
    }
}

