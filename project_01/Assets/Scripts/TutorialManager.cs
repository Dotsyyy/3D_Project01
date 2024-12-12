using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialUI; // sleep hier je png - menu in de inspector

    public void closeTutorial()
    {
        tutorialUI.SetActive(false);
    }
}
