using System;
using System.Collections;
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
    [SerializeField] LayerMask snipeLayerMask;
    public Transform AimTarget => anim.aimIKTarget;
    [SerializeField] float sphereCastRadius = 0.05f;

    [Header("Hold Breath")]
    [SerializeField] float currentBreath;

    float lastFireTime;

    bool isControllingBreath = false;
    bool isReloading = false;
    bool isInCinemachine = false;
    
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

            player.Actions.Aim.started += (context) => { AimStart(); };
            player.Actions.Aim.canceled += (context) => { AimCancel(); };

            player.Actions.Fire.started += (context) => { Fire(); };
            
            player.Actions.ControlBreath.started += (context) => { OnControlBreathStart();};
            player.Actions.ControlBreath.canceled += (context) => { OnControlBreathEnd();};

            player.Condition.OnDead += () => { enabled = false; };

            equip.OnReload += Reload;
            
            currentBreath = player.setting.maxBreath;
        }

    }

    private void Update() 
    {
        if (equip.CurrentEquip && !isReloading)
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


    void AimStart()
    {
        isAiming = true;
        anim.Aiming(isAiming);

        if (equip.CurrentEquip.weaponData.ammoType == AmmoType.SniperAmmo)
            OnAim?.Invoke(true);
    }

    void AimCancel()
    {
        isAiming = false;
        anim.Aiming(false);

        if (equip.CurrentEquip.weaponData.ammoType == AmmoType.SniperAmmo)
            OnAim?.Invoke(false);
    }

    void Fire()
    {
        Weapon weapon = equip.CurrentEquip;
        
        if (Time.time - lastFireTime < weapon.weaponData.fireRate || 
            !weapon.UseAmmo() || isReloading || isInCinemachine)
            return;

        lastFireTime = Time.time;

        // 총알 오브젝트 풀에서 꺼내오기
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.weaponData.projectile.data.type);

        // 사전 검사 - 조준 중에 이번 총격으로 사망하는지 체크
        if (isAiming && CheckTarget(out Transform target)) 
            Snipe(target, weapon, bullet); // 저격 시작
        else // 일반 사격
            bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward, weapon.weaponData.damage, gameObject.tag);
        
        anim.Fire();

        ParticleManager.Instance.SpawnMuzzleFlash(weapon.firePoint);
        SoundManager.Instance.PlaySound(weapon.weaponData.fireSound);

        OnGunFire?.Invoke(weapon.firePoint.position);
    }

    void Aim()
    {
        if(isInCinemachine) return;

        Ray ray = GetRayFromCamera();
        if (Physics.Raycast(ray, out RaycastHit hit , equip.CurrentEquip.weaponData.range, aimLayerMask))
        {
            if (!hit.collider.CompareTag("Player"))
                AimTarget.position = hit.point;
            else
                AimTarget.position = mainCamera.transform.position + mainCamera.transform.forward * 10f;    
        }
        else
        {
            AimTarget.position = mainCamera.transform.position + mainCamera.transform.forward * 10f;    
        }
    }  

    // 저격하기
    void Snipe(Transform target, Weapon weapon, Projectile bullet)
    {
        isInCinemachine = true;

        AimCancel();
        UIManager.Instance.PlayerCanvas.alpha = 0f;

        if (target.TryGetComponent(out Enemy e))
            e.CineDontMove(); // Enemy Pause
        
        bullet.InitializeForCinemachine(weapon.firePoint.position, weapon.firePoint.forward);

        Action actionOnEnd = () =>
        {
            if (target.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(weapon.weaponData.damage); 

            bullet.Release();
            UIManager.Instance.PlayerCanvas.alpha = 1f;
            isInCinemachine = false;
        };

        OnSnipe?.Invoke(bullet.transform, target, weapon.firePoint.position, actionOnEnd);
    }
    
    bool CheckTarget(out Transform target)
    {
        RaycastHit hit;
        
        if (Physics.SphereCast(mainCamera.transform.position, 
            sphereCastRadius, 
            mainCamera.transform.forward, 
            out hit, 
            equip.CurrentEquip.weaponData.range, 
            snipeLayerMask))
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

    void Reload(bool isStart)
    {
        // 재장전 시, 조준 해제
        isReloading = isStart;
        if (isStart)
        {
            // aim 강제 해제
            AimCancel();
        }
    }

    Ray GetRayFromCamera(float zAxisOffset = 3f)
    {
        return new Ray(mainCamera.transform.position + mainCamera.transform.forward * zAxisOffset, mainCamera.transform.forward);
    }


    // 수동으로 총알을 움직이는 코루틴
    // 시네머신에서 쓸 것
    public IEnumerator MoveBullet(Transform bullet, Vector3 firePoint, Transform destination, float travelSpeed)
    {
        float sqrDistance = Vector3.SqrMagnitude(firePoint - destination.position);
        float travelTime = sqrDistance / (travelSpeed * travelSpeed);
        travelTime = travelTime < 1f ? 1f : travelTime;

        float elapsedTime = 0f;

        Vector3 end = destination.position;

        while (elapsedTime < travelTime)
        {
            elapsedTime += Time.fixedDeltaTime;
            bullet.position = Vector3.Lerp(firePoint, end, elapsedTime / travelTime);
            
            yield return null;
        }

        bullet.position = destination.position;
    }

}
