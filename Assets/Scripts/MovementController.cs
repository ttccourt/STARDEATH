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



    Vector3[] GetImpulse()
    {
        float ventralTrans = Input.GetAxis("Horizontal");
        float lateralTrans = Input.GetAxis("Lateral");
        float cranialTrans = Input.GetAxis("Vertical");
        float pitch = Input.GetAxis("Pitch");
        float roll = Input.GetAxis("Roll");
        float yaw = Input.GetAxis("Yaw");

        if (rollDisabled) roll = 0;  //if roll disabled, don't report 'fake roll'

        //if (playerFuelLevelController.FuelLevel <= 0) return new float[] { 0, 0, 0, 0, 0, 0 };  // if no fuel, don't report impossible impulse
        if (playerFuelLevelController.FuelLevel <= 0) return new Vector3[] { Vector3.zero, Vector3.zero };

        //return new float[] { ventralTrans, lateralTrans, cranialTrans, pitch, roll, yaw };
        return new Vector3[] { new Vector3(ventralTrans, lateralTrans, cranialTrans), new Vector3(pitch, roll, yaw) };
    }

    public float GetTotalImpulse()
    {
        Vector3[] impulse = GetImpulse();
        float ret = 0;
        for (int i=0; i<3; i++)
        {
            ret += Mathf.Abs(impulse[0][i]) + Mathf.Abs(impulse[1][i]);
        }
        //foreach (float direction in GetImpulse())
        //{
        //    float absDirection = Mathf.Abs(direction);
        //    if (absDirection > 0)
        //    {
        //        totalImpulse += absDirection;
        //    }
        //}
        return ret;
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
            rb.AddRelativeForce(GetImpulse()[0] * translationalThrust);
            rb.AddRelativeTorque(GetImpulse()[1] * translationalThrust);
            //rb.AddRelativeForce(new Vector3(lateralTrans * translationalThrust, cranialTrans * translationalThrust, ventralTrans * translationalThrust));
            //rb.AddRelativeTorque(new Vector3(pitch * rotationalThrust, yaw * rotationalThrust, roll * rotationalThrust));
        }
    }
}
