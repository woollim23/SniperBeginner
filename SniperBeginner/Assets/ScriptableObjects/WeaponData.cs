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
    public float reloadTime;
    public float fireRate;
    public float range = 50f;

    [Header("Ammunition")]
    public AmmoType ammoType;
    public int magazineSize;
    public int currentAmmoInMagazine;
    public int totalAmmo;

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Sound Settings")]
    public float soundRadius = 10f;

    [Header("Prefabs")]
    public GameObject equipPrefab;
    public GameObject dropPrefab;

    [Header("Inventory Settings")]
    public bool isEquippable = true;
}