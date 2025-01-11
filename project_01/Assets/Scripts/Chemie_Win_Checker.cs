using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemie_Win_Checker : MonoBehaviour
{
    public Animator anim;
    private Renderer paperRenderer;
    public Material winMaterial;
    bool inTrigger = false;
    private bool soundPlayed = false; // Flag to track sound state

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && paperRenderer != null)
        {
            if (paperRenderer.material.color == winMaterial.color && !soundPlayed)
            {
                // right material
                anim.SetBool("youWon", true);
                SoundManager.Instance.PlayAtPosition("trueSound", transform.position);
                soundPlayed = true; // Mark the sound as played
                Timer.instance.StopTimer();
            }
            else if (paperRenderer.material.color != winMaterial.color && !soundPlayed)
            {
                // wrong material
                SoundManager.Instance.PlayAtPosition("wrongSound", transform.position);
                soundPlayed = true; // Mark the sound as played
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PHTag"))
        {
            paperRenderer = other.GetComponent<Renderer>();
            inTrigger = true;
            soundPlayed = false; // Reset the flag when entering the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PHTag"))
        {
            inTrigger = false;
            soundPlayed = false; // Reset the flag when exiting the trigger
        }
    }
}
