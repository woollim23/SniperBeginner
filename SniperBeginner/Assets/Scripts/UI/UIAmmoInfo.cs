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

        weapon = CharacterManager.Instance.Player.GetComponent<Weapon>();

        if (weapon != null)
        {
            weapon.OnAmmoChanged += UpdateWeaponUI;
            UpdateWeaponUI();
        }        
    }

    private void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.OnAmmoChanged -= UpdateWeaponUI;
        }
    }

    private void UpdateWeaponUI()
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

        ammoCountText.text = $"Ammo: {weapon.currentAmmoInMagazine} / {weapon.weaponData.magazineSize}";
    }
}