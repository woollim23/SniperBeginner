using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    PlayerView view;
    PlayerAnimationController anim;
    [SerializeField] List<GameObject> weaponInstance = new List<GameObject>();
    public Weapon CurrentEquip { get; private set; }
    
    // 손 위치
    [SerializeField] Transform rightHand;

    [Header("Temp Place")]
    [SerializeField] QuickSlotManager quickSlotManager;

    bool isReloading = false;
    public event Action<bool> OnReload;
    public event Action<int, int> OnAmmoChanged;


    private void Start() 
    {
        if(TryGetComponent(out Player player))
        {
            view = player.View;
            anim = player.Animation;
        }
        
        for (int i = 0; i < quickSlotManager.allWeapons.Count; i++)
        {
            GameObject instance = Instantiate(quickSlotManager.allWeapons[i].equipPrefab);
            weaponInstance.Add(instance);
        }

        quickSlotManager.OnWeaponSelected += WeaponSelected;
        quickSlotManager.HandleQuickSlotSelection(1);
    }


    private void WeaponSelected(WeaponData data)
    {
        GameObject weapon = Instantiate(data.equipPrefab);
        Equip(weapon.GetComponent<Weapon>());
    }

    public void ModifyWeaponDirection(Vector3 targetPoint)
    {
        // 방향 잡아주기
        if (CurrentEquip != null)
        {
            CurrentEquip.transform.rotation = Quaternion.LookRotation(targetPoint - rightHand.position, Vector3.up);
            
            // 무기 방향 잡을 때 왼손 위치도 보정
            anim.ModifyLeftHandIK(CurrentEquip.lHandPoint.position);
        }
    }


    public void Equip(Weapon equipment)
    {
        if(CurrentEquip != null)
            Unequip();

        CurrentEquip = equipment;
        CurrentEquip.OnAmmoChanged += CallOnAmmoChanged;
        CallOnAmmoChanged(); // 장착 후 초기화

        equipment.transform.SetParent(rightHand);
        equipment.transform.localPosition = Vector3.zero;

        if (!ObjectPoolManager.Instance.projectilePools.ContainsKey(CurrentEquip.weaponData.projectile.data.type))
        {
            ObjectPoolManager.Instance.AddProjectilePool(CurrentEquip.weaponData.projectile);
        }

        view.UpdateAimPosition(equipment.aimPoint);
    }

    public void Unequip()
    {
        view.UpdateAimPosition(null);

        if(CurrentEquip != null)
        {
            // 1안. Destroy 하기 // 2안. 반환하기
            Destroy(CurrentEquip.gameObject);

            CurrentEquip.OnAmmoChanged -= CallOnAmmoChanged;
            CurrentEquip = null;
        }
    }

    public void ReplaceAmmo(int count, AmmoType type)
    {
        // 타입에 따라 퀵슬롯에도 적용
        if(type == CurrentEquip.weaponData.ammoType)
        {
            CurrentEquip.ReplaceMagazine(count);

            ReloadStart();
            Invoke("ReloadEnd", 3.3f);
        }
        else
        {
            // TODO : 퀵슬롯에서 찾아서 넣어주기
        }
    }

    void ReloadStart()
    {
        isReloading = true;
        OnReload?.Invoke(isReloading);
    }

    void ReloadEnd()
    {
        isReloading = false;
        OnReload?.Invoke(isReloading);
    }

    void CallOnAmmoChanged()
    {
        OnAmmoChanged?.Invoke(CurrentEquip.currentAmmoInMagazine, CurrentEquip.weaponData.magazineSize);
    }
}

