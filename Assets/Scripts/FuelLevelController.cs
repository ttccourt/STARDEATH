using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelLevelController : MonoBehaviour
{
    public Text indicator;
    public float startingFuelLevel;
    public float fuelBurnEfficiency = 0.1f;
    public GameObject player;

    private float fuelLevel;
    private MovementController playerMovementController;

    public float FuelLevel
    {
        get
        {
            return fuelLevel;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fuelLevel = startingFuelLevel;
        playerMovementController = player.GetComponent<MovementController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float totalImpulse = playerMovementController.GetTotalImpulse();
        fuelLevel -= totalImpulse * fuelBurnEfficiency;
    }
}
