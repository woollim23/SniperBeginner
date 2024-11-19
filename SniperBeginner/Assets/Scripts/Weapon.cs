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

    public event Action OnAmmoChanged;

    void Start()
    {
        currentAmmoInMagazine = weaponData.magazineSize;
        OnAmmoChanged?.Invoke();
    }

    public bool UseAmmo(int amount)
    {
        if (currentAmmoInMagazine < amount)
        {
            PlaySound(weaponData.emptyFireSound);
            Debug.Log("ź�� ����");
            return false;
        }

        currentAmmoInMagazine -= amount;
        PlaySound(weaponData.fireSound);
        Debug.Log($"ź�� ���: {amount}, ���� ź��: {currentAmmoInMagazine}");
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
        Debug.Log($"������ �Ϸ�: ���� ź�� {currentAmmoInMagazine}");
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