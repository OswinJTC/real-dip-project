using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourScript : MonoBehaviour
{
    // Start is called before the first frame update
    

    public Camera myCamera;
    private GameObject[] SphereTag;
   
    void Start()
    {
        
        SphereTag = GameObject.FindGameObjectsWithTag("Sphere");
        



        for (int i = 0; i < SphereTag.Length; i++)
        {
            if (i%2 == 0)
            {
                SphereTag[i].GetComponent<Renderer>().material.color = Color.red;
               
            }
            else
            {
                SphereTag[i].GetComponent<Renderer>().material.color = Color.blue ;

            }

            //SphereTag[8].GetComponent<Renderer>().material.color = Color.yellow;

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


                
                        Color color = raycastHit.transform.gameObject.GetComponent<Renderer>().material.color;
                        if (color == Color.blue)
                        {
                            raycastHit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                        else
                        {
                            raycastHit.transform.GetComponent<MeshRenderer>().material.color = Color.blue;
                        }

               

                



            }

           



        }
        CheckRed();
        CheckBlue();

    }

    void CheckRed()
    {
        
        for (int i = 0; i < SphereTag.Length; i++)
        {
            if (SphereTag[i].GetComponent<Renderer>().material.color != Color.red)
            {
                return;
            }
        } //All are red, so DO SOMETHING } 

        Debug.Log("YATTA RED");
    }
    void CheckBlue()
    {

        for (int i = 0; i < SphereTag.Length; i++)
        {
            if (SphereTag[i].GetComponent<Renderer>().material.color != Color.blue)
            {
                return;
            }
        } //All are red, so DO SOMETHING } 

        Debug.Log("YATTA BLUE");
    }




}


    
        

   

