using UnityEngine;

public class PourTest : MonoBehaviour
{
    public Bottle bottle1;
    public Bottle bottle2;

    void update()
    {
        bottle1.PourInto(bottle2);
    }
}

