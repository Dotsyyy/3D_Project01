using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public List<GameObject> liquidLayers;  // Tracks the color of each liquid layer
    public int maxLayers = 4;              // Max liquid layers in the bottle
    public List<Material> materialsLiquids;
    public Material targetMaterial;
    public Material nothingMaterial;
    public float soundCooldown = 0f;
    private float lastSoundTime = 0f;

    void Start()
    {
        
        Debug.Log($"{gameObject.name}: Initialized with nothingMaterial set to {nothingMaterial.name}");
    }



    public Material GetTopVisibleMaterial()
    {
        for (int i = liquidLayers.Count - 1; i >= 0; i--)
        {
            GameObject layer = liquidLayers[i];
            if (layer.TryGetComponent<Renderer>(out Renderer renderer))
            {
                Material layerMaterial = renderer.material;
                if (layerMaterial.color == nothingMaterial.color)
                {
                    Debug.Log($"Layer {layer.name} is using the nothing material ({nothingMaterial.name})");
                }
                else
                {
                    Debug.Log($"Top visible material is {layerMaterial.name}");
                    return layerMaterial;
                }

            }
        }
        Debug.Log($"{gameObject.name}: No visible material found, all layers are 'nothing'.");
        return null;
    }

    public bool CanPourInto(Bottle targetBottle)
    {
        Material myTopMaterial = GetTopVisibleMaterial();
        Material targetTopMaterial = targetBottle.GetTopVisibleMaterial();

        bool canPour = (myTopMaterial != null) &&
                       (targetBottle.CountVisibleLayers() < maxLayers) &&
                       (targetTopMaterial == null || targetTopMaterial.color == myTopMaterial.color);

        Debug.Log($"{gameObject.name} CanPourInto {targetBottle.gameObject.name}: {canPour} " +
                  $"(myTopMaterial: {myTopMaterial?.name}, targetTopMaterial: {targetTopMaterial?.name})");
        return canPour;
    }

    private int CountVisibleLayers()
    {
        int visibleLayerCount = 0;
        foreach (GameObject layer in liquidLayers)
        {
            if (layer.TryGetComponent<Renderer>(out Renderer renderer))
            {
                if (renderer.material.color != nothingMaterial.color)
                {
                    visibleLayerCount++;
                }
            }
        }
        Debug.Log($"{gameObject.name}: Count of visible layers is {visibleLayerCount}");
        return visibleLayerCount;
    }


    public void PourInto(Bottle targetBottle)
    {
        Material topMaterial = GetTopVisibleMaterial();
        if (topMaterial == null)
        {
            Debug.Log($"{gameObject.name}: No visible top material to pour.");
            return;
        }

        int layersToPour = CountContiguousTopLayers(topMaterial);
        if (!CanPourMultipleInto(targetBottle, layersToPour, topMaterial))
        {
            Debug.Log($"{gameObject.name}: Pouring into {targetBottle.gameObject.name} is not allowed due to insufficient space or material mismatch.");
            return;
        }

        // Pour each contiguous layer of the same color into the target bottle
        for (int i = 0; i < layersToPour; i++)
        {
            // Find the first non-invisible layer from the top
            for (int j = liquidLayers.Count - 1; j >= 0; j--)
            {
                Renderer layerRenderer = liquidLayers[j].GetComponent<Renderer>();
                if (layerRenderer.material.color != nothingMaterial.color)
                {
                    targetBottle.ChangeMaterial(topMaterial); // Add the material to the target bottle
                    SetLayerInvisible(liquidLayers[j]); // Make the layer invisible in the current bottle
                    break; // Exit the inner loop after finding the first non-invisible layer
                }
            }
        }
        if (Time.time - lastSoundTime >= soundCooldown)
        {
            SoundManager.Instance.PlayAtPosition("pouringSound", transform.position);
            lastSoundTime = Time.time; // Update the last sound play time
            Debug.Log($"Poured into {targetBottle.gameObject.name} and played sound.");
        }
        Debug.Log($"{gameObject.name}: Poured {layersToPour} layers of {topMaterial.name} into {targetBottle.gameObject.name}.");
    }


    // Helper method to count contiguous top layers of the same material
    private int CountContiguousTopLayers(Material topMaterial)
    {
        int count = 0;
        Debug.Log($"{gameObject.name}: Counting contiguous layers of {topMaterial.name} from the top.");

        for (int i = liquidLayers.Count - 1; i >= 0; i--)
        {
            GameObject layer = liquidLayers[i];
            if (layer.TryGetComponent<Renderer>(out Renderer renderer))
            {
                Debug.Log($"{gameObject.name}: Layer {layer.name} at index {i} has material {renderer.material.name}.");

                // If this layer is invisible, skip it and continue checking
                if (renderer.material.color == nothingMaterial.color)
                {
                    Debug.Log($"{gameObject.name}: Layer {layer.name} is invisible, skipping.");
                    continue;
                }

                // If this layer matches the topMaterial, count it as part of the contiguous pour
                if (renderer.material.color == topMaterial.color)
                {
                    count++;
                    Debug.Log($"{gameObject.name}: Contiguous layer count incremented to {count}.");
                }
                else
                {
                    // Stop counting if we hit a different material
                    Debug.Log($"{gameObject.name}: Stopping contiguous layer count at {count}, found different material {renderer.material.name}.");
                    break;
                }
            }
        }
        return count;
    }


    // Modified CanPourInto to support multiple layers and check target space availability
    private bool CanPourMultipleInto(Bottle targetBottle, int layersToPour, Material topMaterial)
    {
        Material targetTopMaterial = targetBottle.GetTopVisibleMaterial();
        bool hasSpace = targetBottle.CountVisibleLayers() + layersToPour <= maxLayers;
        bool materialMatch = targetTopMaterial == null || targetTopMaterial.color == topMaterial.color;

        bool canPourMultiple = hasSpace && materialMatch;

        Debug.Log($"{gameObject.name} CanPourMultipleInto {targetBottle.gameObject.name}: {canPourMultiple} " +
                  $"(layersToPour: {layersToPour}, myTopMaterial: {topMaterial.name}, targetTopMaterial: {targetTopMaterial?.name})");

        return canPourMultiple;
    }



    public void ChangeMaterial(Material newMaterial)
    {
        for (int i = 0; i < liquidLayers.Count; i++)
        {
            Renderer layerRenderer = liquidLayers[i].GetComponent<Renderer>();
            if (layerRenderer.material.color == nothingMaterial.color)
            {
                // Change the first invisible (nothing material) layer to the new material
                layerRenderer.material = newMaterial;
                Debug.Log($"{gameObject.name}: Changed material of first invisible layer at index {i} to {newMaterial.name}");
                return; // Exit after changing the first invisible layer
            }
        }
        Debug.Log($"{gameObject.name}: No available layer to change material to {newMaterial.name}.");
    }


    private void SetLayerInvisible(GameObject layer)
    {
        if (layer.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material = nothingMaterial; // Set layer to the invisible "nothing" material
            Debug.Log($"{gameObject.name}: Set layer {layer.name} to 'nothing' material.");
        }
    }

    public void AddLayer(GameObject layer)
    {
        if (layer.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material = layer.GetComponent<Renderer>().material; // Ensure material remains the same in target bottle
            Debug.Log($"{gameObject.name}: Added layer {layer.name} with material {renderer.material.name}");
        }
    }
}
