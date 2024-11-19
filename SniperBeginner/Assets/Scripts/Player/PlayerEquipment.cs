using System;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    PlayerView view;

    [field:SerializeField] public Weapon CurrentEquip { get; private set; }
    
    // 손 위치
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;

    bool isReloading = false;
    public event Action<bool> OnReload;


    private void Start() 
    {
        if(TryGetComponent(out Player player))
        {
            view = player.View;
        }

        // TODO : 퀵슬롯 1번 장착
        Equip(CurrentEquip);
    }

    private void FixedUpdate() 
    {
        if (isReloading) return;

        if (CurrentEquip != null)
        {
            CurrentEquip.transform.rotation = Quaternion.LookRotation(leftHand.position - rightHand.position, Vector3.up);
        }
    }

    public void Equip(Weapon equipment)
    {
        if(CurrentEquip != null)
            Unequip();

        CurrentEquip = equipment;

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


    // 뼈에서 손 위치만 찾는 메서드
    [ContextMenu("Find Hand")]
    public void FindHand()
    {
        Animator anim = GetComponent<Animator>();

        if (leftHand == null)
            leftHand = new GameObject("Left Hand").transform;
        
        if (rightHand == null)
            rightHand = new GameObject("Right Hand").transform;


        leftHand.SetParent(anim.GetBoneTransform(HumanBodyBones.LeftHand));
        rightHand.SetParent(anim.GetBoneTransform(HumanBodyBones.RightHand));

        leftHand.localPosition = Vector3.zero;
        leftHand.localEulerAngles = Vector3.zero;

        rightHand.localPosition = Vector3.zero;
        rightHand.localEulerAngles = Vector3.zero;
    }


}

