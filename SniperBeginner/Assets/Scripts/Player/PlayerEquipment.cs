using System;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    PlayerView view;
    PlayerAnimationController anim;
    public Weapon CurrentEquip { get; private set; }
    
    // 손 위치
    [SerializeField] Transform rightHand;
    // [SerializeField] Transform leftHand;

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

        quickSlotManager.OnWeaponSelected += WeaponSelected;

        // 퀵슬롯 1번 장착
        WeaponSelected(quickSlotManager.allWeapons[0]);
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

        equipment.transform.SetParent(rightHand);
        equipment.transform.localPosition = Vector3.zero;

        // 장착했다 -> 쓸 수 있다는 것
        // 무기라면 투사체 오브젝트 풀링이 확인해줄 것
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

    // 뼈에서 손 위치만 찾는 메서드
    // [ContextMenu("Find Hand")]
    // public void FindHand()
    // {
    //     Animator anim = GetComponent<Animator>();

    //     if (leftHand == null)
    //         leftHand = new GameObject("Left Hand").transform;
        
    //     if (rightHand == null)
    //         rightHand = new GameObject("Right Hand").transform;


    //     leftHand.SetParent(anim.GetBoneTransform(HumanBodyBones.LeftHand));
    //     rightHand.SetParent(anim.GetBoneTransform(HumanBodyBones.RightHand));

    //     leftHand.localPosition = Vector3.zero;
    //     leftHand.localEulerAngles = Vector3.zero;

    //     rightHand.localPosition = Vector3.zero;
    //     rightHand.localEulerAngles = Vector3.zero;
    // }


}

