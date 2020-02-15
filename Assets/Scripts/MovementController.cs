using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float fuel = 100;
    public float rotationalThrust;
    public float translationalThrust;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float ventralTrans = Input.GetAxis("Ventral");
        float lateralTrans = Input.GetAxis("Lateral");
        float cranialTrans = Input.GetAxis("Cranial");
        float pitch = Input.GetAxis("Pitch");
        float roll = Input.GetAxis("Roll");
        float yaw = Input.GetAxis("Yaw");

        rb.AddRelativeForce(new Vector3(lateralTrans * translationalThrust, cranialTrans * translationalThrust, ventralTrans * translationalThrust));
        rb.AddRelativeTorque(new Vector3(pitch * rotationalThrust, yaw * rotationalThrust, roll * rotationalThrust));

        //rb.MoveRotation(Quaternion.LookRotation(new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 0)));
    }
}
