using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TextSection
    {
        public GameObject MainText;
        public GameObject lastText;
        [HideInInspector] public bool wasActive;
        [HideInInspector] public bool isFinished;
        [HideInInspector] public bool wasActivated;
        [HideInInspector] public float delayTimer; // Timer voor de vertraging
    }

    public TextSection welcomeSection;
    public TextSection buildingNotDoneSection;
    public TextSection buildingIsDoneSection;
    public TextSection carNotDoneSection;
    public TextSection carIsDoneSection;

    public bool buildingTaskIsDone;
    public bool carTaskIsDone;
    public bool tutorialIsFinished;

    void Start()
    {
        InitializeSection(welcomeSection);
        InitializeSection(buildingNotDoneSection);
        InitializeSection(buildingIsDoneSection);

        // Activeer de welkomsttekst na 5 seconden
        Invoke(nameof(ActivateWelcomeSection), 5f);
    }

    void Update()
    {
        CheckSection(welcomeSection, buildingNotDoneSection, 0.5f);
        CheckSection(buildingNotDoneSection, buildingIsDoneSection, 0f, buildingTaskIsDone);
        CheckSection(buildingIsDoneSection, carNotDoneSection, 0.5f);
        CheckSection(carNotDoneSection, carIsDoneSection, 0f, carTaskIsDone);

        if (carTaskIsDone && !tutorialIsFinished)
        {
            StartCoroutine(EndTutorialAfterDelay(6f));
        }
    }

    void InitializeSection(TextSection section)
    {
        if (section.lastText != null)
        {
            section.wasActive = section.lastText.activeInHierarchy;
        }
    }

    public void FitCar()
    {
        carTaskIsDone = true;
    }

    public void PressBuilding()
    {
        buildingTaskIsDone = true;
        Debug.Log("Building task is now done.");
    }

    void CheckSection(TextSection currentSection, TextSection nextSection, float delay, bool condition = true)
    {
        if (currentSection.lastText != null)
        {
            // Controleer of de actieve status is veranderd
            if (currentSection.wasActive && !currentSection.lastText.activeInHierarchy)
            {
                currentSection.isFinished = true;
                currentSection.delayTimer = delay; // Stel de timer in op de opgegeven vertraging
            }

            // Update de status
            currentSection.wasActive = currentSection.lastText.activeInHierarchy;
        }

        // Controleer de timer en activeer de volgende sectie
        if (currentSection.isFinished && !currentSection.wasActivated && condition)
        {
            if (currentSection.delayTimer > 0)
            {
                currentSection.delayTimer -= Time.deltaTime;
            }
            else
            {
                ActivateSection(nextSection);
                currentSection.wasActivated = true; // Zorg ervoor dat dit maar één keer wordt uitgevoerd
                Debug.Log("Activated section: " + nextSection.MainText.name);
            }
        }
    }

    void ActivateWelcomeSection()
    {
        if (welcomeSection.MainText != null)
        {
            welcomeSection.MainText.SetActive(true);
            Debug.Log("Welcome section activated.");
        }
    }

    void ActivateSection(TextSection section)
    {
        // Activeer de hoofdtekst van de volgende sectie
        if (section.MainText != null)
        {
            section.MainText.SetActive(true);
        }
    }

    private IEnumerator EndTutorialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorialIsFinished = true;
        Debug.Log("Tutorial is finished.");
    }
}
