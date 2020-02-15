using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float fuel = 100;
    public float rotationalThrust;
    public float translationalThrust;

    public bool rollDisabled;

    private Rigidbody rb;
    private AudioSource audio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        if (rollDisabled)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void FixedUpdate()
    {
        float ventralTrans = Input.GetAxis("Horizontal");
        float lateralTrans = Input.GetAxis("Lateral");
        float cranialTrans = Input.GetAxis("Vertical");
        float pitch = Input.GetAxis("Pitch");
        float roll = Input.GetAxis("Roll");
        float yaw = Input.GetAxis("Yaw");

        if (rollDisabled) roll = 0;
  
        if (ventralTrans + lateralTrans + cranialTrans + pitch + roll + yaw != 0) {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        } else
        {
            audio.Stop();
        }

        rb.AddRelativeForce(new Vector3(lateralTrans * translationalThrust, cranialTrans * translationalThrust, ventralTrans * translationalThrust));
        rb.AddRelativeTorque(new Vector3(pitch * rotationalThrust, yaw * rotationalThrust, roll * rotationalThrust));
    }
}
