using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public Vector3 Position;
    public float Health;
    public string EquippedWeaponName;
    public int CurrentAmmoInMagazine;
}
