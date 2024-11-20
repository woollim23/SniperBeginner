using System.Collections.Generic;
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
    public float range = 1000f;

    [Header("Ammunition")]
    public int magazineSize; 
    public AmmoType ammoType;     

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Prefabs")]
    public GameObject equipPrefab;

    [Header("Projectile")]
    public Projectile projectile; 

    [Header("Audio Clips")]
    public AudioClip fireSound;
    public AudioClip emptyFireSound;
    public AudioClip reloadSound;
    public AudioClip equipSound;
}