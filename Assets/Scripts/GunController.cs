﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject[] guns;
    public GameObject selectedGun;
    public GameObject muzzle;
    Animator anim;

    private void Fire()
    {
        GameObject bullet = Instantiate(selectedGun.GetComponent<Gun>().bullet, muzzle.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,0,10));
    }

    void SelectGun(int gun)
    {
        selectedGun.SetActive(false);
        selectedGun = guns[gun];
        selectedGun.SetActive(true);
        anim = selectedGun.GetComponent<Animator>();
        anim.SetTrigger("Draw Gun");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Select Pistol"))
        {
            SelectGun(0);
        }
        if (Input.GetButtonDown("Select SMG"))
        {
            SelectGun(1);
        }
        if (Input.GetButtonDown("Select Rifle"))
        {
            SelectGun(2);
        }
        if (Input.GetButtonDown("Select Sniper"))
        {
            SelectGun(3);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Fire();
        }
    }
}
