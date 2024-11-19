using System;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    PlayerAnimationController anim;
    PlayerEquipment equip;
    Camera mainCamera;

    [Header("Aim Setting")]
    public bool isAiming;
    [SerializeField] LayerMask aimLayerMask;
    [SerializeField] Transform aimIKTarget; // IK point로 쓸 것
    float lastFireTime;

    bool isReloading = false;

    public event Action<Transform, Vector3, Vector3> OnKilledEnemy;

    private void Awake() 
    {
        if(!aimIKTarget)
            aimIKTarget = new GameObject("Aim IK Target").transform;
        
        aimIKTarget.SetParent(null);
    }

    private void Start() 
    {
        mainCamera = Camera.main;

        if (TryGetComponent(out Player player))
        {
            anim = player.Animation;
            equip = player.Equipment;

            player.Actions.Aim.started += (context) => {AimStarted();};
            player.Actions.Aim.canceled += (context) => {AimCanceled();};

            player.Actions.Fire.started += (context) => {Fire();};

            equip.OnReload += Reload;
        }
    }

    private void Update() 
    {
        if (equip.CurrentEquip)
            Aim();
    }


    void AimStarted()
    {
        isAiming = true;
        anim.Aiming(isAiming);
    }

    void AimCanceled()
    {
        isAiming = false;
        anim.Aiming(false);
    }

    void Fire()
    {
        Weapon weapon = equip.CurrentEquip;
        if (Time.time - lastFireTime < weapon.weaponData.fireRate || 
            !weapon.UseAmmo() ||
            isReloading)
            return;

        lastFireTime = Time.time;
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.weaponData.projectile.data.type);

        // 사전 검사 - 이번 총격으로 사망했는지?
        if (Check(out Transform target))
        {
            // 검사에서 사망했다 -> 시네머신 : 시네머신에서 죽일 것
            Debug.Log("시네머신 시작");
            OnKilledEnemy?.Invoke(bullet.transform, weapon.firePoint.position, target.position);
        }
        else
        {
            // 검사에서 사망하지 않았다 -> 아래 코드 : 물리적으로 공격
            bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward);
        }

        anim.Fire();
    }

    void Aim()
    {
        aimIKTarget.position = mainCamera.transform.position + mainCamera.transform.forward * 10f;
    }

    bool Check(out Transform target)
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit , float.PositiveInfinity, aimLayerMask))
        {
            if (hit.collider.TryGetComponent(out ISnipable snipable))
            {
                target = hit.collider.transform;
                return snipable.CheckRemainHealth() <= equip.CurrentEquip.weaponData.damage;
            }
        }

        target = null;
        return false;
    }

    void Reload(bool isStart)
    {
        isReloading = isStart;
        if(isStart)
        {
            // aim 강제 해제
            AimCanceled();
            // 모션 재생
            anim.Reload();

            Debug.Log("reload");
        }
    }

}
