using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial rotation of the GameObject
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Lock the rotation to the initial rotation
        transform.rotation = initialRotation;
    }
}
