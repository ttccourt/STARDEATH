using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Container : MonoBehaviour
{
    public float OxyLevel { get; private set; }
    public float startingOxyLevel = 50;

    public float consume(float maxQuantity)
    {
        if (maxQuantity <= OxyLevel)
        {
            OxyLevel = OxyLevel - maxQuantity;
            return maxQuantity;
        } else
        {
            float ret = OxyLevel;
            OxyLevel = 0;
            return ret;
        }
    }

    void Start()
    {
        OxyLevel = startingOxyLevel;
    }
}
