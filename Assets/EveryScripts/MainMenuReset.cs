using UnityEngine;

public class MainMenuReset : MonoBehaviour
{
    public ButtonImageChanger[] buttonChangers; // Assign each button's ButtonImageChanger component
    public ButtonHover[] buttonHovers; // Assign each button with ButtonHover component

    void OnEnable()
    {
        // Reset each button to its default state
        foreach (ButtonImageChanger changer in buttonChangers)
        {
            changer.ResetToDefault();
        }

        // Reset each button's text appearance to its default state
        foreach (ButtonHover hover in buttonHovers)
        {
            hover.ResetToDefault();
        }
    }
}