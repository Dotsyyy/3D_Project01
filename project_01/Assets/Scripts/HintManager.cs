using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> initialHints;
    [SerializeField] private List<GameObject> waterSortHints;
    [SerializeField] private List<GameObject> finalHints;

    private int checkOne = 0;
    private int checkTwo = 0;
    private int checkThree = 0;

    public bool waterSortDone;
    public bool liquidTested;


    public void waterBool()
    {
        waterSortDone = true;
    }

    public void liquidBool()
    {
        liquidTested = true;
    }

    public void HintSystem()
    {
        if (!waterSortDone && !liquidTested)
            ActivateHint(initialHints, ref checkOne);
        else if (waterSortDone && !liquidTested)
            ActivateHint(waterSortHints, ref checkTwo);
        else if (waterSortDone && liquidTested)
            ActivateHint(finalHints, ref checkThree);
    }


    private void ActivateHint(List<GameObject> hints, ref int check)
    {
        if (check < hints.Count)
        {
            var hint = hints[check];
            hint.SetActive(true);
            StartCoroutine(WaitForCompletionAndDeactivate(hint));
            check++;
        }
    }

    private IEnumerator WaitForCompletionAndDeactivate(GameObject hint)
    {
        AudioSource audioSource = hint.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            while (audioSource.isPlaying)
                yield return null;
        }
        hint.SetActive(false);
    }
}

