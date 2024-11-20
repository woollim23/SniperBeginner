using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    Player player;
    PlayerAnimationController anim;
    PlayerEquipment equip;
    Camera mainCamera;

    [Header("Aim Setting")]
    public bool isAiming;
    [SerializeField] LayerMask aimLayerMask;
    public Transform AimTarget => anim.aimIKTarget;

    [Header("Hold Breath")]
    [SerializeField] float currentBreath;

    float lastFireTime;

    bool isControllingBreath = false;
    bool isReloading = false;
    
    public event Action<float> OnControlBreath;
    public event Action<bool> OnAim;
    public event Action<Vector3> OnGunFire;
    public event Action<Transform, Vector3, Transform> OnKilledEnemy;


    private void Start() 
    {
        mainCamera = Camera.main;

        if (TryGetComponent(out Player player))
        {
            this.player = player;
            anim = player.Animation;
            equip = player.Equipment;

            player.Actions.Aim.started += (context) => { AimStarted(); };
            player.Actions.Aim.canceled += (context) => { AimCanceled(); };

            player.Actions.Fire.started += (context) => { Fire(); };
            player.Actions.ControlBreath.started += (context) => { OnControlBreathStart();};
            player.Actions.ControlBreath.canceled += (context) => { OnControlBreathEnd();};

            equip.OnReload += Reload;
            
            currentBreath = player.setting.maxBreath;
        }

    }

    private void Update() 
    {
        if (equip.CurrentEquip)
        {
            Aim();
            equip.ModifyWeaponDirection(AimTarget.position);
        }

        if (isControllingBreath)
            UseBreath();
        else
            RecoverBreath();
    }


    void RecoverBreath()
    {
        if (currentBreath < player.setting.maxBreath)
            currentBreath += player.setting.breathAmountOnRelax * Time.deltaTime;
        else
            currentBreath = player.setting.maxBreath;

        OnControlBreath?.Invoke(currentBreath / player.setting.maxBreath);
    }

    void UseBreath()
    {
        currentBreath -= player.setting.decayBreathWhileControl * Time.deltaTime;
        
        if (currentBreath <= 0f)
        {
            // 강제 종료
            OnControlBreathEnd();
        }

        OnControlBreath?.Invoke(currentBreath / player.setting.maxBreath);
    }


    void AimStarted()
    {
        isAiming = true;
        anim.Aiming(isAiming);

        OnAim?.Invoke(true);
    }

    void AimCanceled()
    {
        isAiming = false;
        anim.Aiming(false);

        OnAim?.Invoke(false);
    }

    void Fire()
    {
        Weapon weapon = equip.CurrentEquip;
        if (Time.time - lastFireTime < weapon.weaponData.fireRate || 
            !weapon.UseAmmo() ||
            isReloading)
            return;

        lastFireTime = Time.time;
        
        anim.Fire();
        // 겉으로 표시만 하는 용도
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.weaponData.projectile.data.type);
        bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward);

        // 레이 방식으로 변경
        // mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f));
        Ray ray = new Ray(weapon.firePoint.position, weapon.firePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, weapon.weaponData.range, aimLayerMask))
        {
            if (hitInfo.collider.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(weapon.weaponData.damage);
                Debug.Log($"Ray hit : {hitInfo.collider.name}");
            }
        }

        return;
        

        // 사전 검사 - 이번 총격으로 사망했는지?
        if (CheckTarget(out Transform target))
        {
            // 검사에서 사망했다 -> 시네머신 : 시네머신에서 죽일 것
            Debug.Log("시네머신 시작");
            OnKilledEnemy?.Invoke(bullet.transform, equip.CurrentEquip.firePoint.position, target);
        }
        else
        {
            // 검사에서 사망하지 않았다 -> 아래 코드 : 물리적으로 공격
            bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward);
        }
        OnGunFire?.Invoke(transform.position);
    }

    void Aim()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit , Mathf.Infinity, aimLayerMask))
        {
            AimTarget.position = hit.point;
        }
        else
        {
            AimTarget.position = mainCamera.transform.position + mainCamera.transform.forward * 10f;    
        }
    }  

    bool CheckTarget(out Transform target)
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit , Mathf.Infinity, aimLayerMask))
        {
            // 부위별 총격에서 데미지 확인 각각의
            if (hit.collider.TryGetComponent(out ISnipable snipable))
            {
                target = hit.collider.transform;
                return snipable.IsSnipable(equip.CurrentEquip.weaponData.damage);
            }
        }

        target = null;
        return false;
    }

    void OnControlBreathStart()
    {
        if (currentBreath < player.setting.minBreathForControl) 
            return;

        isControllingBreath = true;
        anim.AimingModifier(0.05f);
    }

    void OnControlBreathEnd()
    {
        isControllingBreath = false;
        anim.AimingModifier(1f);
    }


    // TODO : Equip에 있어도 될거 같음
    void Reload(bool isStart)
    {
        isReloading = isStart;
        if(isStart)
        {
            // aim 강제 해제
            AimCanceled();
            // 모션 재생
            anim.Reload();
        }
    }



}
