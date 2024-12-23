using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringHitBox : MonoBehaviour
{
    public Bottle bottle;
    private float pourAngleThreshold = 80f;
    public float rayDistance = 2f; // Adjust distance as needed
    public float soundCooldown = 2f; // Cooldown time in seconds between sound triggers
    private bool isInitialized = false;
    private float lastSoundTime = 0f; // Tracks the last time the sound was played
    public List<GameObject> allBottlesRayCasting;
    private List<Vector3> originalPositions = new List<Vector3>();
    private List<Quaternion> originalRotations = new List<Quaternion>();

    void Start()
    {
        // Start the initialization coroutine
        StartCoroutine(InitializePosition());
    }

    private IEnumerator InitializePosition()
    {
        // Wait for 1 or 2 seconds before capturing position and rotation
        yield return new WaitForSeconds(4f); // You can adjust the time here

        foreach (var bottle in allBottlesRayCasting) 
        { 
            originalPositions.Add(bottle.transform.position); 
            originalRotations.Add(bottle.transform.rotation); 
        }

        isInitialized = true;
        Debug.Log("Initial position and rotation set.");
    }

    void Update()
    {
        // Only proceed if initialization is done
        if (isInitialized && IsInOriginalPosition() && CheckForPouring())
        {
            PerformPouring();
        }
    }

    private bool IsInOriginalPosition()
    {
        for (int i = 0; i < allBottlesRayCasting.Count; i++)
        {
            if (Vector3.Distance(allBottlesRayCasting[i].transform.position, originalPositions[i]) < 0.01f &&
                Quaternion.Angle(allBottlesRayCasting[i].transform.rotation, originalRotations[i]) < 1f)
            {
                return true;
            }
        }
        return false;
    }


    private bool CheckForPouring()
    {
        RaycastHit hit;
        Vector3 rayDirection = transform.forward;

        if (Physics.Raycast(transform.position, rayDirection, out hit, rayDistance))
        {
            Bottle targetBottle = hit.collider.GetComponent<Bottle>();

            if (targetBottle != null)
            {
                float angle = GetPouringAngle(targetBottle.gameObject);
                Debug.Log($"{targetBottle.gameObject.name} hit the ray with an angle of {angle} degrees.");

                if (IsPouringAngle(targetBottle.gameObject))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void PerformPouring()
    {
        RaycastHit hit;
        Vector3 rayDirection = transform.forward;

        if (Physics.Raycast(transform.position, rayDirection, out hit, rayDistance))
        {
            Bottle targetBottle = hit.collider.GetComponent<Bottle>();

            if (targetBottle != null)
            {
                targetBottle.PourInto(bottle);
                if (Time.time - lastSoundTime >= soundCooldown)
                {
                    
                }
            }
        }
    }

    private bool IsPouringAngle(GameObject pouringObject)
    {
        float angle = GetPouringAngle(pouringObject);
        return angle <= pourAngleThreshold;
    }

    private float GetPouringAngle(GameObject pouringObject)
    {
        // Get the world direction representing "pouring" relative to the initial prefab orientation
        Vector3 pouringDirection = pouringObject.transform.forward;

        // Calculate the angle between the pouring direction and downward
        float angle = Vector3.Angle(pouringDirection, Vector3.down);

        return angle;
    }

    void OnDrawGizmos()
    {
        // Visualize the ray in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
