using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject[] guns;
    public GameObject selectedGun;

    void SelectGun(int gun)
    {
        selectedGun.SetActive(false);
        selectedGun = guns[gun];
        selectedGun.SetActive(true);
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
}
