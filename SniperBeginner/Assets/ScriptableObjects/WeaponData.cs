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
    public AmmoType ammoType;          // ź�� ����

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Prefabs")]
    public GameObject equipPrefab;     // ���� ������

    [Header("Projectile")]
    public Projectile projectile;      // �߻�ü        
}