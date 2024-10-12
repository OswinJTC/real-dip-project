using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class phonepuzzle : MonoBehaviour // Capitalize class name for convention
{
    public TextMeshProUGUI displayText;
    private string currentInput = "";
    private string correctPassword = ".-.. . ..-. -"; // Change to string for the correct password

    public void OnButtonClick(string buttonValue)
    {
        if (buttonValue == "=")
        {
            CalculateResult();
        }
        else
        {
            currentInput += buttonValue;
            UpdateDisplay();
        }

        ClearInput(); // Call ClearInput to reset after a certain length
    }

    public void CalculateResult()
    {
        if (currentInput == correctPassword) // Compare current input with correct password
        {
            Debug.Log("Phone Unlocked!"); // Use Debug.Log instead of print for better practice
        }
        else
        {
            Debug.Log("Wrong Password!");
            currentInput = ""; // Clear current input
        }
        UpdateDisplay(); // Update display after checking the result
    }

    private void ClearInput()
    {
        if (currentInput.Length >= 14)
        {
            currentInput = "";
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        displayText.text = currentInput; // Update the display text
    }
}
