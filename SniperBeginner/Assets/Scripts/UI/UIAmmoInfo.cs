using UnityEngine;
using TMPro;

public class UIAmmoInfo : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI ammoCountText;

    private Weapon weapon;

    private void Start()
    {
        if (CharacterManager.Instance.Player == null)
        {
            return;
        }

        //CharacterManager.Instance.Player.Equipment.OnAmmoChanged += UpdateWeaponUI;      
    }

    private void OnDisable() 
    {
        //CharacterManager.Instance.Player.Equipment.OnAmmoChanged -= UpdateWeaponUI;
    }


    private void UpdateWeaponUI(int curAmmo, int maxAmmo)
    {
        if (ammoCountText == null)
        {
            return;
        }

        if (weapon == null || weapon.weaponData == null)
        {
            ammoCountText.text = "No Weapon";
            return;
        }

        ammoCountText.text = $"{curAmmo} / {maxAmmo}";
    }
}