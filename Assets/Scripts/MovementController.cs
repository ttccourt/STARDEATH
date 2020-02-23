using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float fuelLevel = 100;
    public float fuelMax = 100;
    public float fuelBurnSpeed = 0.1f;

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

        float absTotalImpulse = 0;
        foreach (float direction in new float[]{ventralTrans, lateralTrans, cranialTrans, pitch, roll, yaw})
        {
            float absDirection = Mathf.Abs(direction);
            if (absDirection > 0)
            {
                absTotalImpulse += absDirection;
                fuelLevel -= absDirection * fuelBurnSpeed;
                Debug.Log(fuelLevel);
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
            }
        }
        if (absTotalImpulse == 0)
        {
            audio.Stop();
        }
  

        rb.AddRelativeForce(new Vector3(lateralTrans * translationalThrust, cranialTrans * translationalThrust, ventralTrans * translationalThrust));
        rb.AddRelativeTorque(new Vector3(pitch * rotationalThrust, yaw * rotationalThrust, roll * rotationalThrust));
    }
}
