using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActivateSomething : MonoBehaviour
{
    public GameObject opject;
    void OnEnable()
    {
        opject.SetActive(false);
    }
}
