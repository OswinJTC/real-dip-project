using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{

    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Awake()
    {
       for (int i = 0; i < 8; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }
    }
}
