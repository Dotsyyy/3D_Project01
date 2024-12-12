using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOtherScript : MonoBehaviour
{
    public MonoBehaviour[] targetScripts;

    void OnEnable()
    {
        // Loop through each script in the array and enable it.
        foreach (MonoBehaviour script in targetScripts)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }
    }


}
