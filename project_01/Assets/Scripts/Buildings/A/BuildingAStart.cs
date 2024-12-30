using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingAStart : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public GameObject building;
    public string sceneName = "MCT";
    public GameObject outlineBuilding;
    public Animator animator;
    private bool isPressed = false;

    void Update()
    {
        if (!CanProceed()) return;
    }

    private bool CanProceed()
    {
        if (tutorialManager != null && tutorialManager.tutorialIsFinished)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void loadScene()
    {
        if (!CanProceed()) return;
        SceneManager.LoadScene(sceneName);
    }

    public void GebouwClicked()
    {
        if (!CanProceed()) return;

        if (!BuildingManager.instance.isAnyBuildingActive)
        {
            // Activate building
            building.SetActive(true);
            outlineBuilding.SetActive(true);
            animator.SetBool("Move", true);
            BuildingManager.instance.isAnyBuildingActive = true;
            isPressed = true;
        }
        else if (isPressed)
        {
            animator.SetBool("Small", true);
        }

    }

    public void ChangeBool()
    {
        isPressed = true;
    }

    public void WhenEvent()
    {
        outlineBuilding.SetActive(false);
        isPressed = false;
        animator.SetBool("Move", false);
        animator.SetBool("Small", false);
        building.SetActive(false);
        BuildingManager.instance.isAnyBuildingActive = false;
    }
}
