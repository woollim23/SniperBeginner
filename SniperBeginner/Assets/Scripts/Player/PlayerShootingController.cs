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

    public event Action<Transform, Transform> OnKilledEnemy;


    private void Awake() 
    {
        anim = GetComponent<PlayerAnimationController>();
        equip = GetComponent<PlayerEquipment>();

        if(!aimIKTarget)
            aimIKTarget = new GameObject("Aim IK Target").transform;
        
        aimIKTarget.SetParent(null);
    }

    private void Start() 
    {
        mainCamera = Camera.main;

        if (TryGetComponent(out Player player))
        {
            player.Actions.Aim.started += (context) => {AimStarted();};
            player.Actions.Aim.canceled += (context) => {AimCanceled();};

            player.Actions.Fire.started += (context) => {Fire();};
        }
    }

    private void Update() 
    {
        if(equip.CurrentEquip)
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
        if (Time.time - lastFireTime < equip.CurrentEquip.data.fireRate) 
            return;

        if(!equip.CurrentEquip.HasAmmo()) return;

        lastFireTime = Time.time;
        anim.Fire();

        Weapon weapon = equip.CurrentEquip;
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.data.projectile.data.type);

        // 사전 검사 - 이번 총격으로 사망했는지?
        if (Check(out Transform targetT))
        {
            // 검사에서 사망했다 -> 시네머신 : 시네머신에서 죽일 것
            Debug.Log("시네머신 시작");
            OnKilledEnemy?.Invoke(bullet.transform, targetT);
        }
        else
        {
            // 검사에서 사망하지 않았다 -> 아래 코드 : 물리적으로 공격
            bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward);
        }
    }

    void Aim()
    {
        aimIKTarget.position = mainCamera.transform.position + mainCamera.transform.forward * 10f;
    }

    bool Check(out Transform targetT)
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit , float.PositiveInfinity, aimLayerMask))
        {
            if (hit.collider.TryGetComponent(out ISnipable target))
            {
                targetT = hit.collider.transform;
                return target.CheckRemainHealth() <= equip.CurrentEquip.data.damage;
            }
        }

        targetT = null;
        return false;
    }
}
