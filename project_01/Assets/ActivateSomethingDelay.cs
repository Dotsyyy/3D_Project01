using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSomethingDelay : MonoBehaviour
{
    public GameObject opject; // Consider renaming "opject" to "objectToActivate" for better readability.
    public float delay; // Delay time in seconds.

    void OnEnable()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay.
        opject.SetActive(true); // Activate the object.
    }
}
