using UnityEngine;

public class PHIndicator : MonoBehaviour
{
    public Material sourMaterial;  // Material to use when the smaller object is inside the large cube for the required time
    private Renderer paperRenderer;

    private bool isInCube = false;
    private float timer = 0f;
    public float requiredTimeInCube = 3f; // Time required inside the large cube to trigger color change

    void Start()
    {
        // Start without assigning any material because we will change it dynamically
    }

    void Update()
    {
        // Check if the small cube is inside the large cube
        if (isInCube && paperRenderer != null)
        {
            // Increment the timer while inside the large cube
            timer += Time.deltaTime;

            // If the small cube stays inside for the required time, change its material
            if (timer >= requiredTimeInCube)
            {
                paperRenderer.material = sourMaterial;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the small cube (tagged "SmallCube")
        if (other.CompareTag("PHTag"))
        {
            // Get the Renderer of the small cube to change its material
            paperRenderer = other.GetComponent<Renderer>();
            isInCube = true;
        }
    }
}
