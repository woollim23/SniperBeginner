using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmoData", menuName = "Data/AmmoData")]
public class AmmoData : ScriptableObject
{
    [Header("Ammo Info")]
    public string ammoName;
    public string ammoDescription;
    public AmmoType ammoType;
    public int maxAmmo;
    public Sprite icon;
}