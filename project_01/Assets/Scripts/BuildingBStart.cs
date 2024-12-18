using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingBStart : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public GameObject buildingB;
    public string sceneName = "Chemie";
    public GameObject outlineBuilding;

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
    public void loadScene()
    {
        if (!CanProceed()) return;
        SceneManager.LoadScene(sceneName);
    }


    // Method to be called by the button
    public void GebouwClicked()
    {
        if (!CanProceed()) return;

        buildingB.SetActive(true);
        outlineBuilding.SetActive(true);
    }
}
