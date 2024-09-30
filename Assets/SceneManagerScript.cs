using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    public string scenename;

    void OnMouseDown()
    {
        SceneManager.LoadScene(scenename);
    }
}
