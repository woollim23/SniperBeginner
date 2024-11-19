using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponData weaponData;

    [Header("Ammo")]
    public int currentAmmoInMagazine;

    [Header("Zoom Settings")]
    public Transform firePoint;
    public Transform aimPoint;

    void Start()
    {
        currentAmmoInMagazine = weaponData.magazineSize;
    }

    public bool UseAmmo(int amount)
    {
        if (currentAmmoInMagazine < amount)
        {
            Debug.Log("ź�� ����");
            return false;
        }

        currentAmmoInMagazine -= amount;
        Debug.Log($"ź�� ���: {amount}, ���� ź��: {currentAmmoInMagazine}");
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

        Debug.Log($"������ �Ϸ�: ���� ź�� {currentAmmoInMagazine}");
    }

    public bool HasAmmo()
    {
        return currentAmmoInMagazine > 0;
    }
}