using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesScript : MonoBehaviour
{

    private Vector3 RightPosition;
    public bool InRightPos;
    public bool Selected;
    

    // Start is called before the first frame update
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(-14f, 14f),Random.Range(43f,52f) ,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, RightPosition) < 1f)
        {
            if (!Selected)
            {
                transform.position = RightPosition;
                InRightPos = true;
                
            }
        }
    }
}
