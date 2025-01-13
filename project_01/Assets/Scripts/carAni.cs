using System.Collections;
using UnityEngine;

public class carAni : MonoBehaviour
{
    // Veranderbare parameters voor de animatie
    public Vector3 startScale = new Vector3(1, 1, 1);  // Beginformaat
    public Vector3 endScale = new Vector3(2, 0.5f, 1); // Eindformaat
    public float animationDuration = 2f;               // Duur van de animatie
    public bool loopAnimation = true;                  // Herhaal animatie?

    private bool isAnimating = false;                  // Controle of animatie bezig is

    void Start()
    {
        // Start de animatie wanneer het spel begint
        if (loopAnimation)
        {
            StartCoroutine(AnimateAutoLoop());
        }
        else
        {
            StartCoroutine(AnimateAuto());
        }
    }

    IEnumerator AnimateAuto()
    {
        // Zorg voor een soepele overgang tussen startScale en endScale
        isAnimating = true;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float progress = elapsedTime / animationDuration;
            transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
        isAnimating = false;
    }

    IEnumerator AnimateAutoLoop()
    {
        while (true)
        {
            yield return AnimateAuto();
            // Wissel start en eindformaat zodat de animatie "terugveert"
            Vector3 temp = startScale;
            startScale = endScale;
            endScale = temp;
        }
    }
}
