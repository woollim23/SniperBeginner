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
    public float reloadTime;           // ������ �ð�
    public float fireRate;             // �߻� �ӵ�

    [Header("Ammunition")]
    public int magazineSize;           // źâ �ִ� ũ��
    public int currentAmmoInMagazine;  // ���� źâ�� ���� �Ѿ�
    public AmmoType ammoType;          // ź�� ����

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Prefabs")]
    public GameObject equipPrefab;     // ���� ������

    [Header("Zoom Settings")]
    public Transform firePoint;
    public Transform aimPoint;

    // �Ѿ� �߻� �õ� �޼���
    public bool UseAmmo(int amount)
    {
        if (currentAmmoInMagazine < amount)
        {
            Debug.Log("ź�� ����");
            return false;
        }

        currentAmmoInMagazine -= amount;
        return true;
    }

    // źâ ��ü �޼���
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

    // ���� �Ѿ��� ���� �ִ��� Ȯ��
    public bool HasAmmo()
    {
        return currentAmmoInMagazine > 0;
    }
}