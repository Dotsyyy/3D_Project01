using UnityEngine;

public class HoverScript : MonoBehaviour
{
    [Header("Hover Settings")]
    public float hoverHeight = 0.1f; // How far the object moves up and down
    public float hoverSpeed = 2.0f; // Speed of the hover motion

    private Vector3 initialPosition;

    void Start()
    {
        // Save the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position based on sine wave
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(initialPosition.x, initialPosition.y + hoverOffset, initialPosition.z);
    }
}