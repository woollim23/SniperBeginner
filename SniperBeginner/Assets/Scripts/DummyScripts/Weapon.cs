using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int currentAmmoInMagazine;  // 현재 탄창에 남은 총알
    public WeaponData data;


    [Header("Zoom Settings")]
    public Transform firePoint;
    public Transform aimPoint;
    

    // 총알 발사 시도 메서드
    public bool UseAmmo(int amount)
    {
        if (currentAmmoInMagazine < amount)
        {
            Debug.Log("탄약 부족");
            return false;
        }

        currentAmmoInMagazine -= amount;
        return true;
    }

    // 탄창 교체 메서드
    public void ReplaceMagazine(int newAmmo)
    {
        int totalAmmo = currentAmmoInMagazine + newAmmo;

        if (totalAmmo > data.magazineSize)
        {
            int excessAmmo = totalAmmo - data.magazineSize;
            currentAmmoInMagazine = data.magazineSize;
        }
        else
        {
            currentAmmoInMagazine = totalAmmo;
        }
    }

    // 현재 총알이 남아 있는지 확인
    public bool HasAmmo()
    {
        return currentAmmoInMagazine > 0;
    }
}
