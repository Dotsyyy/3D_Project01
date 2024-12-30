using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventD : MonoBehaviour
{
    public BuildingDStart buildingDStart;


    public void triggerEvent()
    {
        buildingDStart.WhenEvent();
    }

    public void EventBool()
    {
        buildingDStart.ChangeBool();
    }
}
