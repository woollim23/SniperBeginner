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

    public bool UseAmmo(int amount = 1)
    {
        if (currentAmmoInMagazine < amount)
        {
            Debug.Log("탄약 부족");
            return false;
        }

        currentAmmoInMagazine -= amount;
        Debug.Log($"탄약 사용: {amount}, 남은 탄약: {currentAmmoInMagazine}");
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

        Debug.Log($"재장전 완료: 현재 탄약 {currentAmmoInMagazine}");
    }

    public bool HasAmmo()
    {
        return currentAmmoInMagazine > 0;
    }
}