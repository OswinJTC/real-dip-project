using UnityEngine;

public class TimeTravelController : MonoBehaviour
{
    public GameObject presentObjects; // Reference to the PresentRoom (parent GameObject)
    public GameObject pastObjects;    // Reference to the PastRoom (parent GameObject)

    private string currentPeriod = "present"; // Default to present

    void Update()
    {
        // Check if the "Q" key is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleTimePeriod();
        }
    }

    // Function to toggle between present and past
    void ToggleTimePeriod()
    {
        if (currentPeriod == "present")
        {
            SwitchToPast();
        }
        else
        {
            SwitchToPresent();
        }
    }

    // Function to switch to past
    void SwitchToPast()
    {
        currentPeriod = "past"; 
        pastObjects.SetActive(true);     // Enable past room
        presentObjects.SetActive(false); // Disable present room
    }

    // Function to switch to present
    void SwitchToPresent()
    {
        currentPeriod = "present";
        presentObjects.SetActive(true);  // Enable present room
        pastObjects.SetActive(false);    // Disable past room
    }
}
