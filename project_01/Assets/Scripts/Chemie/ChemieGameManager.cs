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
    private List<Vector3> startPositions = new List<Vector3>(); // List to hold start positions
    private List<Quaternion> startRotations = new List<Quaternion>(); // List to hold start rotations
    private bool isInitialized = false;

    public HintManager hintManager;
    public Animator stokjesAnimator;
    public List<GameObject> stokjes;

    void Start()
    {
        // Start the initialization coroutine
        StartCoroutine(InitializeStartPositions());
    }

    private IEnumerator InitializeStartPositions()
    {
        // Wait for the specified delay before capturing start positions and rotations
        yield return new WaitForSeconds(5f);

        // Store start positions and rotations of each bottle
        foreach (Bottle bottle in bottles)
        {
            startPositions.Add(bottle.transform.position);
            startRotations.Add(bottle.transform.rotation);
        }

        isInitialized = true;
        Debug.Log("Initial positions and rotations set.");
    }

    void Update()
    {
        if (isInitialized && CheckWinCondition() && !hasSwapped)
        {
            Debug.Log("Congratulations! You won the game!");
            SoundManager.Instance.Play("codeAppears");
            StartCoroutine(SwapPositions());
            hasSwapped = true; // Set the flag to true to prevent further swaps
            hintManager.waterBool();

            foreach  (GameObject stok in stokjes)
            {
                stok.SetActive(true);
            }

            stokjesAnimator.SetBool("Move",true);
        }
    }

    // Checks if each bottle is at its start position and filled with its target color only
    private bool CheckWinCondition()
    {
        for (int i = 0; i < bottles.Count; i++)
        {
            if (!IsBottleAtStartPosition(bottles[i], startPositions[i], startRotations[i]) || !IsBottleUniformWithTarget(bottles[i]))
            {
                return false;
            }
        }     
        return true;
    }

    // Checks if a bottle is at its start position and rotation
    private bool IsBottleAtStartPosition(Bottle bottle, Vector3 startPosition, Quaternion startRotation)
    {
        bool isAtStartPosition = Vector3.Distance(bottle.transform.position, startPosition) < 0.01f;
        bool isAtStartRotation = Quaternion.Angle(bottle.transform.rotation, startRotation) < 1f;

        Debug.Log($"{bottle.gameObject.name} is at start position: {isAtStartPosition}, is at start rotation: {isAtStartRotation}");

        return isAtStartPosition && isAtStartRotation;
    }

    // Determines if all visible layers in a bottle match its target color
    private bool IsBottleUniformWithTarget(Bottle bottle)
    {
        foreach (GameObject layer in bottle.liquidLayers)
        {
            if (layer.TryGetComponent<Renderer>(out Renderer renderer))
            {
                Material currentMaterial = renderer.material;

                // If an invisible layer is found, the bottle is not full
                if (currentMaterial.color == bottle.nothingMaterial.color)
                {
                    return false; // Bottle is not completely full
                }

                // If a layer's color does not match the target color, it's invalid
                if (currentMaterial.color != bottle.targetMaterial.color)
                {
                    return false; // Mismatch found
                }
            }
        }

        // If all layers are visible and match the target color, return true
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
