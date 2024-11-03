using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesertes : MonoBehaviour
{
    public Bottle bottle1;
    public Bottle bottle2;
    public Bottle bottle3;

    void Start()
    {
        StartCoroutine(SequentialPours());
    }

    private IEnumerator SequentialPours()
    {
        yield return StartCoroutine(DelayedPour());         // Call the first pour after 2 seconds
        yield return StartCoroutine(EvenMoreDelayedPour()); // Call the second pour after 10 seconds
        yield return StartCoroutine(DelayedPour());         // Call the third pour after the previous pour (2 seconds)
        yield return StartCoroutine(EvenMoreDelayedPour()); // Call the fourth pour after the previous pour (10 seconds)
    }

    private IEnumerator DelayedPour()
    {
        yield return new WaitForSeconds(2f); // Waits for 2 seconds
        bottle1.PourInto(bottle2);
        Debug.Log("It did the pouring after 5 seconds");
    }

    private IEnumerator EvenMoreDelayedPour()
    {
        yield return new WaitForSeconds(10f); // Waits for 10 seconds
        bottle2.PourInto(bottle3);
        Debug.Log("It did the pouring after 5 seconds");
    }

    void Update()
    {
    }
}
