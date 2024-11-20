using UnityEngine;
using TMPro;

public class UIAmmoInfo : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI ammoCountText;
    
    private void Start()
    {
        if (CharacterManager.Instance.Player == null)
        {
            return;
        }

        CharacterManager.Instance.Player.Equipment.OnAmmoChanged += UpdateWeaponUI;
    }

    // private void OnDisable() 
    // {
    //     CharacterManager.Instance.Player.Equipment.OnAmmoChanged -= UpdateWeaponUI;
    // }


    private void UpdateWeaponUI(int curAmmo, int maxAmmo)
    {
        if (ammoCountText == null)
        {
            return;
        }

        ammoCountText.text = $"{curAmmo} / {maxAmmo}";
    }
}