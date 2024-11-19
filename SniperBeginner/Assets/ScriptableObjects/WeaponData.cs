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
    public AmmoType ammoType;          // 탄약 유형

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Prefabs")]
    public GameObject equipPrefab;     // 장착 프리팹

    [Header("Projectile")]
    public Projectile projectile;      // 발사체        
}