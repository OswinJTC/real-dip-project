using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foldcarpet : MonoBehaviour
{
    public GameObject foldedVersion1;
    public GameObject foldedVersion2;

    void OnMouseDown()
    {
        Vector3 offset = new Vector3(5f, 0.1f, 0);
        Vector3 offset2 = new Vector3(9f, 0.1f, -1f);
        Quaternion newRotation = Quaternion.Euler(0, 100, 0);
        Instantiate(foldedVersion1, transform.position + offset, newRotation); 
        Instantiate(foldedVersion2, transform.position + offset2, transform.rotation);
        Destroy(gameObject);
    }
}
