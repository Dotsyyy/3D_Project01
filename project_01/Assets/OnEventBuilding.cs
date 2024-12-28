using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventBuilding : MonoBehaviour
{
    public BuildingBStart buildingBStart;


    public void triggerEvent()
    {
        buildingBStart.WhenEvent();
    }

    public void EventBool()
    {
        buildingBStart.ChangeBool();
    }
}
