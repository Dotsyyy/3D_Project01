using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TextManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> textObjects; // List of text objects to display in sequence
    private int currentIndex = 0;

    void Start()
    {
        // Deactivate all text objects at the start
        foreach (var textObj in textObjects)
        {
            textObj.SetActive(false);
        }

        Debug.Log("All text objects deactivated at start");

        // Start displaying the first text object
        if (textObjects.Count > 0)
        {
            Debug.Log("Starting to display the first text object");
            ActivateNextText();
        }
        else
        {
            Debug.Log("No text objects found in the list");
        }
    }

    // Method to activate the next text object in the list
    public void ActivateNextText()
    {
        if (currentIndex < textObjects.Count)
        {
            // Activate the current text object
            textObjects[currentIndex].SetActive(true);
            Debug.Log("Activated text object: " + textObjects[currentIndex].name);

            // Get the TypeWriter component and start the typing effect
            TypeWriter typeWriter = textObjects[currentIndex].GetComponent<TypeWriter>();
            if (typeWriter != null)
            {
                Debug.Log("TypeWriter component found, starting typing effect");
                StartCoroutine(WaitForCompletion(typeWriter));
            }
            else
            {
                Debug.Log("TypeWriter component not found on the text object");
            }

            currentIndex++;
        }
        else
        {
            Debug.Log("No more text objects to display");
        }
    }

    // Coroutine to wait for the TypeWriter effect to complete
    private IEnumerator WaitForCompletion(TypeWriter typeWriter)
    {
        // Wait until the TypeWriter effect is finished
        while (!typeWriter.isFinished)
        {
            yield return null;
        }

        Debug.Log("TypeWriter effect finished for: " + textObjects[currentIndex - 1].name);

        // Deactivate the current text object
        textObjects[currentIndex - 1].SetActive(false);

        // Activate the next text object
        if (currentIndex < textObjects.Count)
        {
            ActivateNextText();
        }
    }
}
