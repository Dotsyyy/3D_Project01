using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heeelp : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public GameObject helpText;

    void Update()
    {
        if (!CanProceed()) return;
    }

    private bool CanProceed()
    {
        return tutorialManager != null && tutorialManager.tutorialIsFinished;
    }

    public void Heeelp()
    {
        if (!CanProceed()) return;

        helpText.SetActive(true);
        StartCoroutine(HideHelpTextAfterDelay());
    }

    private IEnumerator HideHelpTextAfterDelay()
    {
        yield return new WaitForSeconds(6f); // Wait for 6 seconds
        helpText.SetActive(false);
    }
}
