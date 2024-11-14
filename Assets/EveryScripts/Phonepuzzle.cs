using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Add SceneManager

public class PhonePuzzle : MonoBehaviour // Capitalize class name for convention
{
    public TextMeshProUGUI displayText;
    private string currentInput = "";
    //private string correctPassword = ".-.. . ..-. -"; // Change to string for the correct password
    private string correctPassword = "."; // Change to string for the correct password

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
            GameManager.instance.SaveMonsterPosition(SceneManager.GetActiveScene().name);

            // Set the phone's active status in the GameManager
            GameManager.instance.isPhoneActive = true;
            GameManager.instance.UpdateInventoryUI(); // Update the inventory UI to reflect the change
            UIManager.instance.ShowPrompt("Phone collected...all the best for the puzzle...", 2f);
            SceneManager.LoadScene("BedroomScene");

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

    private void Update()
    {
        // Check if "L" is pressed to leave the game and return to the saved position in "BedroomScene"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Exiting the puzzle and returning to the real room...");

            GameManager.instance.SaveMonsterPosition(SceneManager.GetActiveScene().name);

            SceneManager.LoadScene(GameObject.FindObjectOfType<ClayChange>().previousRealScene);
            
        }
    }

    private IEnumerator RestorePlayerPosition()
    {
        yield return new WaitForEndOfFrame(); // Wait for the scene to load
        if (GameManager.instance != null && GameManager.instance.savedPlayerPosition.HasValue)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = GameManager.instance.savedPlayerPosition.Value;
                GameManager.instance.savedPlayerPosition = null; // Clear saved position after restoring
            }
        }
    }
}
