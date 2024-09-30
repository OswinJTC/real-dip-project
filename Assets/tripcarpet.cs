using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tripcarpet : MonoBehaviour
{
    public GameObject trippedVersion;

    void OnMouseDown ()
    {
        Vector3 offset = new Vector3(-2f, 0, 0);
        //Quaternion newRotation = Quaternion.Euler(0, 30, 0);
        Instantiate(trippedVersion, transform.position + offset, transform.rotation);
        Destroy(gameObject);
    }
}
