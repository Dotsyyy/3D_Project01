using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class MoveBuildingUp : MonoBehaviour
{
    public GameObject buildingA;       // Sleep hier gebouw A in de Inspector
    public float moveDistance = 5f;    // Afstand waarmee gebouw omhoog beweegt
    public float moveSpeed = 2f;       // Snelheid van beweging

    private bool shouldMove = false;
    private Vector3 startPosition;

    private void Start()
    {
        // Startpositie van gebouw A vastleggen
        startPosition = buildingA.transform.position;
    }

    public void MoveBuilding()
    {
        // Start de beweging als de knop wordt ingedrukt
        shouldMove = true;
    }

    private void Update()
    {
        // Beweeg gebouw A omhoog zolang shouldMove true is
        if (shouldMove)
        {
            Vector3 targetPosition = startPosition + Vector3.up * moveDistance;
            buildingA.transform.position = Vector3.MoveTowards(buildingA.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop de beweging wanneer de targetpositie bereikt is
            if (buildingA.transform.position == targetPosition)
            {
                shouldMove = false;
            }
        }
    }
}

