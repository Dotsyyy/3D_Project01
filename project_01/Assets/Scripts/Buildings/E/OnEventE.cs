using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventE : MonoBehaviour
{
    public BuildingEStart buildingEStart;


    public void triggerEvent()
    {
        buildingEStart.WhenEvent();
    }

    public void EventBool()
    {
        buildingEStart.ChangeBool();
    }
}
