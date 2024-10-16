using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    public void OnHourGlassButton()
    {
        SceneManager.LoadScene(2);
    }


}
