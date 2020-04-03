using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public float rotationalThrust;  // how quickly do we pitch/yaw/roll?
    public float translationalThrust;  // how quickly do we translate?

    public float speedAlertThreshold;
    public Text cranialSpeedometer;
    public Text lateralSpeedometer;
    public Text ventralSpeedometer;

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
        if (playerFuelLevelController.FuelLevel <= 0) return new Vector3[] { Vector3.zero, Vector3.zero };  // if no fuel, don't report fake impulse

        return new Vector3[] { new Vector3(lateralTrans, cranialTrans, ventralTrans), new Vector3(pitch, yaw, roll) };
    }

    public float GetTotalImpulse()
    {
        Vector3[] impulse = GetImpulse();
        float ret = 0;
        for (int i=0; i<3; i++)
        {
            ret += Mathf.Abs(impulse[0][i]) + Mathf.Abs(impulse[1][i]);
        }
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

    private void Update()
    {
        // handle RCS noises
        if (Mathf.Approximately(GetTotalImpulse(), 0))
        {
            rcsAudio.Stop();
        } else if (GetTotalImpulse() > 0 && !rcsAudio.isPlaying)
        {
            rcsAudio.Play();
        }

        // update speedometers
        // this uses LOCAL reference frame... 
        // so, if the player doesn't arrest their rotation, they will be confused...
        Vector3 localSpeed = transform.InverseTransformDirection(rb.velocity);
        lateralSpeedometer.text = localSpeed.x.ToString("000");
        cranialSpeedometer.text = localSpeed.y.ToString("000");
        ventralSpeedometer.text = localSpeed.z.ToString("000");
    }

    void FixedUpdate()
    {
        // apply relevant forces every physics step
        if (playerFuelLevelController.FuelLevel > 0)
        {
            rb.AddRelativeForce(GetImpulse()[0] * translationalThrust);
            rb.AddRelativeTorque(GetImpulse()[1] * translationalThrust);
        }
    }
}
