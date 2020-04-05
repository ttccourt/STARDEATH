using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenController : MonoBehaviour
{
    public Text indicator;

    public float startingOxyLevel = 50;
    public float oxyPerMinute = 5;
    public float oxyAlertThreshold = 0.1f;

    private bool indicatorFlashing = false;


    // allow OxyLevel to be read but not written by other scripts
    public float OxyLevel { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        OxyLevel = startingOxyLevel;
        InvokeRepeating("DoFlashIfSet", 0, 0.333333f);
    }

    void DoFlashIfSet()
    {
        if (indicatorFlashing)
        {
            if (indicator.color == Color.white)
            {
                indicator.color = Color.red;
            }
            else
            {
                indicator.color = Color.white;
            }
        }

    }

    void FixedUpdate()
    {
        OxyLevel -= (oxyPerMinute / 60) * Time.fixedDeltaTime;
        indicator.text = OxyLevel.ToString("000");
    }

    void Update()
    {
        if (OxyLevel < startingOxyLevel * oxyAlertThreshold)
        {
            indicatorFlashing = true;
        }
        else
        {
            indicatorFlashing = false;
        }
    }
}
