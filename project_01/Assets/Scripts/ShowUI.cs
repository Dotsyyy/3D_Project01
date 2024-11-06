using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIOnHover : MonoBehaviour
{
    public GameObject uiElement;

    // Deze functie wordt aangeroepen wanneer de pointer het object binnenkomt
    public void ShowUI()
    {
        uiElement.SetActive(true);
    }

    // Deze functie wordt aangeroepen wanneer de pointer het object verlaat
    public void HideUI()
    {
        uiElement.SetActive(false);
    }
}


