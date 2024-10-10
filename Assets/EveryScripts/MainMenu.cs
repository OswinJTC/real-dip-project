using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNewGameButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnContinueButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnControlsButton()
    {
        SceneManager.LoadScene(2);
    }
    public void OnExitButton()
    {
        Application.Quit();
    }

}
