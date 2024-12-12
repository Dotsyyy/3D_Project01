using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTask : MonoBehaviour
{
    public GameObject otherCube; // Sleep hier de andere kubus in vanuit de inspector.
    public TutorialManager tutorialManager;
    public float positionTolerance = 0.1f; // Tolerantie voor positie
    public float rotationTolerance = 5f;   // Tolerantie voor rotatie in graden

    private void Update()
    {
        if (TransformsAreEqual())
        {
            tutorialManager.FitCar();
        }
    }

    private bool TransformsAreEqual()
    {
        // Vergelijk positie en rotatie met tolerantie.
        return Vector3.Distance(transform.position, otherCube.transform.position) < positionTolerance &&
               Quaternion.Angle(transform.rotation, otherCube.transform.rotation) < rotationTolerance;
    }
}

