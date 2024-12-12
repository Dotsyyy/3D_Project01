using UnityEngine;

public class BuildingBStart : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public GameObject gebouwB;
    public GameObject door;

    void Update()
    {
        if (!CanProceed()) return;


    }

    // Centralized check method
    private bool CanProceed()
    {
        if (tutorialManager != null && tutorialManager.tutorialIsFinished)
        {
            return true;
        }
        else
        {
            Debug.Log("Action cannot proceed because the tutorial is not finished.");
            return false;
        }
    }

    // Example method that depends on the tutorial being finished
    public void ExampleMethod()
    {
        if (!CanProceed()) return;

        // Your method logic here
        Debug.Log("Executing ExampleMethod logic.");
    }

    // Another example method
    public void AnotherMethod()
    {
        if (!CanProceed()) return;

        // Your method logic here
        Debug.Log("Executing AnotherMethod logic.");
    }

    // Method to be called by the button
    public void ButtonClicked()
    {
        if (!CanProceed()) return;

        // Logic to be executed when the button is clicked and the tutorial is finished
        Debug.Log("ButtonClicked logic executed.");
    }
}
