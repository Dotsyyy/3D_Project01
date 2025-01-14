using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDiplo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject opject;

    public void Activation()
    {
        opject.SetActive(true);
    }
}
