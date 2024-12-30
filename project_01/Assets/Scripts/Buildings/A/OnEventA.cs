using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventA : MonoBehaviour
{
    public BuildingAStart buildingAStart;


    public void triggerEvent()
    {
        buildingAStart.WhenEvent();
    }

    public void EventBool()
    {
        buildingAStart.ChangeBool();
    }
}
