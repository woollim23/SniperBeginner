using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Info")]
    public string weaponName;
    public string weaponDescription;
    public Sprite icon;

    [Header("Combat Stats")]
    public int damage;
    public float reloadTime;           // 재장전 시간
    public float fireRate;             // 발사 속도

    [Header("Ammunition")]
    public int magazineSize;           // 탄창 최대 크기
    public int currentAmmoInMagazine;  // 현재 탄창에 남은 총알
    public AmmoType ammoType;          // 탄약 유형

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Prefabs")]
    public GameObject equipPrefab;     // 장착 프리팹

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

        if (totalAmmo > magazineSize)
        {
            int excessAmmo = totalAmmo - magazineSize;
            currentAmmoInMagazine = magazineSize;
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