using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float rotationalThrust;  // how quickly do we pitch/yaw/roll?
    public float translationalThrust;  // how quickly do we translate?

    public bool rollDisabled;
    // rollDisabled sets a constraint, and disables reporting of roll input.
    //
    // This is only a temporary solution, and once a settings menu is implemented,
    // this functionality will be moved to some SettingsController script.

    public GameObject player;  // to access other scripts attached to player
    private FuelLevelController playerFuelLevelController;

    private Rigidbody rb;
    private AudioSource rcsAudio;



    float[] GetImpulse()
    {
        float ventralTrans = Input.GetAxis("Horizontal");
        float lateralTrans = Input.GetAxis("Lateral");
        float cranialTrans = Input.GetAxis("Vertical");
        float pitch = Input.GetAxis("Pitch");
        float roll = Input.GetAxis("Roll");
        float yaw = Input.GetAxis("Yaw");

        if (rollDisabled) roll = 0;  //if roll disabled, don't report 'fake roll'

        if (playerFuelLevelController.FuelLevel <= 0) return new float[] { 0, 0, 0, 0, 0, 0 };  // if no fuel, don't report impossible impulse

        return new float[] { ventralTrans, lateralTrans, cranialTrans, pitch, roll, yaw };
    }

    public float GetTotalImpulse()
    {
        float totalImpulse = 0;
        foreach (float direction in GetImpulse())
        {
            float absDirection = Mathf.Abs(direction);
            if (absDirection > 0)
            {
                totalImpulse += absDirection;
            }
        }
        return totalImpulse;
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rcsAudio = GetComponent<AudioSource>();

        playerFuelLevelController = player.GetComponent<FuelLevelController>();

        if (rollDisabled)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void FixedUpdate()
    {

        if (Mathf.Approximately(GetTotalImpulse(), 0))
        {
            rcsAudio.Stop();
        }
        else if (GetTotalImpulse() > 0 && !rcsAudio.isPlaying)
        {
            rcsAudio.Play();
        }

        if (playerFuelLevelController.FuelLevel > 0)
        {
            //rb.AddRelativeForce(new Vector3(lateralTrans * translationalThrust, cranialTrans * translationalThrust, ventralTrans * translationalThrust));
            //rb.AddRelativeTorque(new Vector3(pitch * rotationalThrust, yaw * rotationalThrust, roll * rotationalThrust));
        }
    }
}
