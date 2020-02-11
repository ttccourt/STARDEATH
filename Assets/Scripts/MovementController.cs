using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float fuel = 100;
    public float rotationalThrust = 1;
    public float translationalThrust = 1;

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

        rb.AddForce(new Vector3(lateralTrans * translationalThrust, 0, ventralTrans * translationalThrust));
    }
}
