using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyWeapon : MonoBehaviour
{
    public Transform firePoint;
    public Transform aimPoint;

    // weapon data
    [Header("temp weapon data")]
    public Projectile projectile;
    public int ammoCount = 30;
    public float intervalTime = 1f;
    public float reloadTime = 1f;
}
