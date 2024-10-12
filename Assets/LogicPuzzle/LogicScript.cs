using UnityEngine;

public class LogicScript : MonoBehaviour
{

    public Camera myCamera;
    private GameObject[] LogicTag;
    void Start()
    {
        LogicTag = GameObject.FindGameObjectsWithTag("Logic");

        for (int i = 0; i < LogicTag.Length; i++)
        {

            LogicTag[i].GetComponent<Renderer>().material.color = Color.red;

        }
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetMouseButtonDown(0))
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
          
            CheckGreen();
           
        }


        
        void CheckGreen()
        {

        if ((LogicTag[1].GetComponent<Renderer>().material.color != Color.red) && (LogicTag[4].GetComponent<Renderer>().material.color != Color.red))
        {
            if ((LogicTag[0].GetComponent<Renderer>().material.color != Color.green) && (LogicTag[2].GetComponent<Renderer>().material.color != Color.green))
            {
                if (LogicTag[3].GetComponent<Renderer>().material.color != Color.green)
                {
                    LogicTag[5].GetComponent<MeshRenderer>().material.color = Color.green;
                    Debug.Log("You win");
                }
            }
        }


        return;

    }

}
