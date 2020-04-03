using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelLevelController : MonoBehaviour
{
    public Text indicator;

    public float startingFuelLevel;
    public float fuelBurnEfficiency = 0.1f;
    public float fuelAlertThreshold = 0.1f;

    public GameObject player;
    private MovementController playerMovementController;

    private bool indicatorFlashing = false;

    // allow FuelLevel to be read but not written by other scripts
    public float FuelLevel { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        FuelLevel = startingFuelLevel;
        playerMovementController = player.GetComponent<MovementController>();
        InvokeRepeating("DoFlashIfSet", 0, 0.333333f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float totalImpulse = playerMovementController.GetTotalImpulse();
        FuelLevel -= totalImpulse * fuelBurnEfficiency;
        indicator.text = FuelLevel.ToString("000");
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


    void Update()
    {
        if (FuelLevel < startingFuelLevel * fuelAlertThreshold)
        {
            indicatorFlashing = true;
        } else
        {
            indicatorFlashing = false;
        }
    }
}
