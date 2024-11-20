using System;
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
    public event Action<Transform, Transform, Vector3, Action> OnSnipe;


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

    private void OnDisable() 
    {
        OnAim = null;
        OnControlBreath = null;
        OnGunFire = null;
        OnSnipe = null;
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

        // 겉으로 표시만 하는 용도
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.weaponData.projectile.data.type);

        // 사전 검사 - 조준 중에 이번 총격으로 사망했는지?
        if (isAiming && CheckTarget(out Transform target))
        {
            AimCanceled();
            
            bullet.InitializeForCinemachine
            (
                weapon.firePoint.position,
                weapon.firePoint.forward
            );

            OnSnipe?.Invoke(
                bullet.transform,
                target,
                weapon.firePoint.position,
                ()=>
                {
                    if (target.TryGetComponent(out IDamagable damagable))
                        damagable.TakeDamage(weapon.weaponData.damage); 

                    bullet.Release();
                }
            );
        }
        else
        {
            bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward, weapon.weaponData.damage);
            // 다시 투사체 방식으로 변경
            // Ray ray = GetRayFromCamera(0f);
            // if (Physics.Raycast(ray, out RaycastHit hitInfo, weapon.weaponData.range, aimLayerMask))
            // {
            //     if (hitInfo.collider.TryGetComponent(out IDamagable damagable))
            //     {
            //         damagable.TakeDamage(weapon.weaponData.damage);
            //     }

            //     bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward, weapon.weaponData.damage);
            // }
        }
        
        anim.Fire();

        ParticleManager.Instance.SpawnMuzzleFlash(weapon.firePoint);
        SoundManager.Instance.PlaySound(weapon.weaponData.fireSound);

        OnGunFire?.Invoke(weapon.firePoint.position);
    }

    void Aim()
    {
        Ray ray = GetRayFromCamera();
        if (Physics.Raycast(ray, out RaycastHit hit , equip.CurrentEquip.weaponData.range, aimLayerMask))
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
        Ray ray = GetRayFromCamera(0f);
        if (Physics.Raycast(ray, out RaycastHit hit , equip.CurrentEquip.weaponData.range, aimLayerMask))
        {
            Debug.Log($"hit : {hit.collider.name}");
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

    void Reload(bool isStart)
    {
        // 재장전 시, 조준 해제
        isReloading = isStart;
        if (isStart)
        {
            // aim 강제 해제
            AimCanceled();
        }
    }


    Ray GetRayFromCamera(float zAxisOffset = 3f)
    {
        return new Ray(mainCamera.transform.position + mainCamera.transform.forward * zAxisOffset, mainCamera.transform.forward);
    }

}
