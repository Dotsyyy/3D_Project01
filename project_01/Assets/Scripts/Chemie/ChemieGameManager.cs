using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemieGameManager : MonoBehaviour
{
    public List<Bottle> bottles; // List of all bottles in the game

    void Update()
    {
        if (CheckWinCondition())
        {
            Debug.Log("Congratulations! You won the game!");
            // Add any additional win logic here
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
}
