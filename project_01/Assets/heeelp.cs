using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heeelp : MonoBehaviour
{

    public TutorialManager tutorialManager;
    public GameObject staticText;
    public GameObject helpText;


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

    public void Heeelp()
    {
        if (!CanProceed()) return;

        staticText.SetActive(false);
        helpText.SetActive(true);

        //after 6 seconds

        staticText.SetActive(true);
        helpText.SetActive(false);

    }
}
