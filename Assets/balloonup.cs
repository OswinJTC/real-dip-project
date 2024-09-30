using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonup : MonoBehaviour
{
    public GameObject balloon;

    void onMouseDown()
    {
        Vector3 offset = new Vector3(0, 6f, -0.1f);
        GameObject instantiatedBalloon = Instantiate(balloon, transform.position + offset, transform.rotation);

        float scaleFactor = 2.0f; // Adjust this value to change the size
        instantiatedBalloon.transform.localScale *= scaleFactor; // Scale the b
    }
}
