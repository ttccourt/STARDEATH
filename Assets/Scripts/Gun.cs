using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float bulletsPerSecond;
    public float bulletSpeed;
    public int bulletDamage;

    public int maxBulletsLoaded;
    public int bulletsLoaded;
    public int bulletsHeld;

    public Vector3 recoilTranslate;
    public Vector3 recoilRotate;

    public bool scope;
    public GameObject bullet;
}
