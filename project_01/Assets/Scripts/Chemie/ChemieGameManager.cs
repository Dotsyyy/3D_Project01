using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemieGameManager : MonoBehaviour
{
    public List<Bottle> bottles; // List of all bottles in the game
    public List<GameObject> colours;
    public List<GameObject> numbers;
    public float swapDuration = 1f; // Duration of the swap in seconds

    private bool hasSwapped = false; // Flag to ensure swapping only happens once

    void Update()
    {
        if (CheckWinCondition() && !hasSwapped)
        {
            Debug.Log("Congratulations! You won the game!");
            StartCoroutine(SwapPositions());
            hasSwapped = true; // Set the flag to true to prevent further swaps
        }
    }

    // Checks if each bottle is filled with its target color only
    private bool CheckWinCondition()
    {
        foreach (Bottle bottle in bottles)
        {
            if (!IsBottleUniformWithTarget(bottle))
            {
                return false;
            }
        }
        SoundManager.Instance.Play("codeAppears");
        return true;
        
    }

    // Determines if all visible layers in a bottle match its target color
    private bool IsBottleUniformWithTarget(Bottle bottle)
    {
        foreach (GameObject layer in bottle.liquidLayers)
        {
            if (layer.TryGetComponent<Renderer>(out Renderer renderer))
            {
                Material currentMaterial = renderer.material;

                // Skip invisible layers (those using nothingMaterial)
                if (currentMaterial.color == bottle.nothingMaterial.color)
                {
                    continue;
                }

                // Check if the current layer's color matches the bottle's target color
                if (currentMaterial.color != bottle.targetMaterial.color)
                {
                    return false; // Mismatch found
                }
            }
        }

        // Returns true if all visible layers are the target color (or if the bottle is empty)
        return true;
    }

    // Coroutine to swap positions of colours and numbers
    private IEnumerator SwapPositions()
    {
        List<Vector3> initialColorPositions = new List<Vector3>();
        List<Vector3> initialNumberPositions = new List<Vector3>();

        for (int i = 0; i < colours.Count && i < numbers.Count; i++)
        {
            initialColorPositions.Add(colours[i].transform.position);
            initialNumberPositions.Add(numbers[i].transform.position);
        }

        float elapsedTime = 0;

        while (elapsedTime < swapDuration)
        {
            float t = elapsedTime / swapDuration;

            for (int i = 0; i < colours.Count && i < numbers.Count; i++)
            {
                Vector3 colorTargetPosition = new Vector3(initialColorPositions[i].x, initialColorPositions[i].y, initialNumberPositions[i].z);
                Vector3 numberTargetPosition = new Vector3(initialNumberPositions[i].x, initialNumberPositions[i].y, initialColorPositions[i].z);

                colours[i].transform.position = Vector3.Lerp(initialColorPositions[i], colorTargetPosition, t);
                numbers[i].transform.position = Vector3.Lerp(initialNumberPositions[i], numberTargetPosition, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < colours.Count && i < numbers.Count; i++)
        {
            // Ensure final positions are exactly swapped on the Z-axis
            Vector3 colorTargetPosition = new Vector3(initialColorPositions[i].x, initialColorPositions[i].y, initialNumberPositions[i].z);
            Vector3 numberTargetPosition = new Vector3(initialNumberPositions[i].x, initialNumberPositions[i].y, initialColorPositions[i].z);

            colours[i].transform.position = colorTargetPosition;
            numbers[i].transform.position = numberTargetPosition;
        }
    }
}
