using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sweepway : MonoBehaviour
{
    public GameObject sweptawayVersion;

    void OnMouseDown ()
    {
        Vector3 offset = new Vector3(0, 0, 2f);
        Quaternion newRotation = Quaternion.Euler(-90, 90, 0);
        Instantiate (sweptawayVersion, transform.position + offset, newRotation);
        Destroy(gameObject);
    }
}
