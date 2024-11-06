using UnityEngine;

public class PaperPlacer : MonoBehaviour
{
    public GameObject paperPrefab; // Prefab of the paper object
    public GameObject box; // The cube representing the box
    public int numberOfPapers = 10; // Number of paper objects to place
    public float paperSpacing = 0.1f; // Distance between each paper when stacking

    private void Start()
    {
        StackPapersInBox();
    }

    void StackPapersInBox()
    {
        // Get the bounds of the box
        Bounds boxBounds = box.GetComponent<Collider>().bounds;

        // Start stacking at the bottom center of the box
        Vector3 startPosition = new Vector3(
            boxBounds.min.x + paperSpacing,
            boxBounds.min.y + paperSpacing,
            boxBounds.min.z + paperSpacing
        );

        Vector3 currentPosition = startPosition;
        float paperWidth = paperPrefab.transform.localScale.x + paperSpacing;
        float paperDepth = paperPrefab.transform.localScale.z + paperSpacing;

        int rowCount = 0; // Tracks the number of rows in the current stack level

        for (int i = 0; i < numberOfPapers; i++)
        {
            // Instantiate a new paper object at the current position with a 90-degree rotation on the x-axis
            Instantiate(paperPrefab, currentPosition, Quaternion.Euler(90, 0, 0));

            // Move the position in the X-axis for the next paper
            currentPosition.x += paperWidth;

            // Check if we hit the boundary in the X-axis
            if (currentPosition.x + paperWidth > boxBounds.max.x)
            {
                // Move to the next row in the Z-axis
                currentPosition.x = startPosition.x;
                currentPosition.z += paperDepth;
                rowCount++;

                // If we reach the boundary in the Z-axis, reset Z and move up in the Y-axis
                if (currentPosition.z + paperDepth > boxBounds.max.z)
                {
                    currentPosition.z = startPosition.z;
                    currentPosition.y += paperSpacing; // Move up for a new layer
                    rowCount = 0; // Reset row count for the new layer
                }

                // If Y position exceeds the box height, stop stacking
                if (currentPosition.y + paperSpacing > boxBounds.max.y)
                {
                    Debug.LogWarning("Reached the top of the box. Stopping stack.");
                    break;
                }
            }
        }
    }
}
