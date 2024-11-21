using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    PlayerView view;
    PlayerAnimationController anim;

    public List<WeaponData> allWeapons = new List<WeaponData>(); // 데이터
    [SerializeField] List<Weapon> weaponInstance = new List<Weapon>();

    public Weapon CurrentEquip { get; private set; }
    
    // 손 위치
    [SerializeField] Transform rightHand;


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
        
        for (int i = 0; i < allWeapons.Count; i++)
        {
            GameObject instance = Instantiate(allWeapons[i].equipPrefab);
            instance.SetActive(false);
            weaponInstance.Add(instance.GetComponent<Weapon>());
        }
    }


    public void WeaponSelected(int idx)
    {
        Equip(weaponInstance[idx - 1].GetComponent<Weapon>());
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
        CurrentEquip.gameObject.SetActive(true);

        CurrentEquip.OnAmmoChanged += CallOnAmmoChanged;
        CallOnAmmoChanged(); // 장착 후 초기화

        equipment.transform.SetParent(rightHand);
        equipment.transform.localPosition = Vector3.zero;
        
        ObjectPoolManager.Instance.AddProjectilePool(CurrentEquip.weaponData.projectile);

        view.UpdateAimPosition(equipment.aimPoint);
        anim.SetWeaponType(equipment.weaponData.weaponType);
    }

    public void Unequip()
    {
        view.UpdateAimPosition(null);

        if(CurrentEquip != null)
        {
            CurrentEquip.gameObject.SetActive(false);

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
            foreach (Weapon weapon in weaponInstance)
            {
                if (weapon == CurrentEquip)
                    continue;

                if (weapon.weaponData.ammoType == type)
                {
                    weapon.ReplaceMagazine(count);
                    break;
                }
            }
        }
    }

    void ReloadStart()
    {
        isReloading = true;
        OnReload?.Invoke(isReloading);
        
        // 모션 재생
        anim.rig.weight = 0f;
        anim.Reload();
    }

    void ReloadEnd()
    {
        isReloading = false;
        OnReload?.Invoke(isReloading);

        anim.rig.weight = 1f;
    }

    void CallOnAmmoChanged()
    {
        OnAmmoChanged?.Invoke(CurrentEquip.currentAmmoInMagazine, CurrentEquip.weaponData.magazineSize);
    }
}

