using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponData weaponData;

    [Header("Ammo")]
    public int currentAmmoInMagazine;

    [Header("Zoom Settings")]
    public Transform firePoint;
    public Transform aimPoint;
    public Transform lHandPoint;

    public event Action OnAmmoChanged;

    private void Awake() 
    {
        currentAmmoInMagazine = weaponData.magazineSize;
    }

    public bool UseAmmo(int amount = 1)
    {
        if (currentAmmoInMagazine < amount)
        {
            PlaySound(weaponData.emptyFireSound);
            return false;
        }

        currentAmmoInMagazine -= amount;
        OnAmmoChanged?.Invoke();
        return true;
    }

    public void ReplaceMagazine(int newAmmo)
    {
        int totalAmmo = currentAmmoInMagazine + newAmmo;

        if (totalAmmo > weaponData.magazineSize)
        {
            int excessAmmo = totalAmmo - weaponData.magazineSize;
            currentAmmoInMagazine = weaponData.magazineSize;
        }
        else
        {
            currentAmmoInMagazine = totalAmmo;
        }

        PlaySound(weaponData.reloadSound);
        OnAmmoChanged?.Invoke();
    }

    public bool HasAmmo()
    {
        return currentAmmoInMagazine > 0;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            SoundManager.Instance.PlaySound(clip);
        }
    }
}