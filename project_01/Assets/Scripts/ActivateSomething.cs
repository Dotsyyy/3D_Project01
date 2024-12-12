using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSomething : MonoBehaviour
{
    public GameObject opject;
    void OnEnable()
    {
        opject.SetActive(true);
    }
}
