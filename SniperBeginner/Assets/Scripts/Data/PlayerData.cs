using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 Position;
    public float Health;
    public int EquippedWeaponIndex; // 장비 리스트 사용
    public int CurrentAmmoInMagazine;
}
