using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemie_Win_Checker : MonoBehaviour
{
    public Animator anim;
    private Renderer paperRenderer;
    public Material winMaterial;
    bool inTrigger = false;

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && paperRenderer.material.color == winMaterial.color)
        {
            //right material
            anim.SetBool("youWon", true);
        }
        if (inTrigger && paperRenderer.material.color != winMaterial.color)
        {
            //wrong material

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the small cube (tagged "SmallCube")
        if (other.CompareTag("PHTag"))
        {
            // Get the Renderer of the small cube to change its material
            paperRenderer = other.GetComponent<Renderer>();
            inTrigger = true;
        }
    }
}
