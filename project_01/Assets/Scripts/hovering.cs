using UnityEngine;

public class HoverAndRotateScript : MonoBehaviour
{
    [Header("Hover Settings")]
    public float hoverHeight = 0.1f; // Hoe ver het object op en neer beweegt
    public float hoverSpeed = 2.0f;  // Snelheid van de hoverbeweging

    [Header("Rotation Settings")]
    public float rotationSpeed = 45.0f; // Snelheid van de rotatie in graden per seconde

    private Vector3 initialPosition;

    void Start()
    {
        // Sla de beginpositie van het object op
        initialPosition = transform.position;
    }

    void Update()
    {
        // Bereken de nieuwe positie gebaseerd op een sinusgolf
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(initialPosition.x, initialPosition.y + hoverOffset, initialPosition.z);

        // Roteer het object rond zijn y-as
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
