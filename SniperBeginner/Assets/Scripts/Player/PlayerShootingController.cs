using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    PlayerAnimationController anim;
    PlayerEquipment equip;

    bool isAiming;

    Camera mainCamera;
    [SerializeField] Transform aimPoint; // IK point로 쓸 것

    [Header("Setting")]
    [SerializeField] LayerMask aimLayerMask;
    [SerializeField] float checkRate = 0.05f;
    float lastCheckTime;


    private void Awake() 
    {
        anim = GetComponent<PlayerAnimationController>();
        equip = GetComponent<PlayerEquipment>();

        if(!aimPoint)
            aimPoint = new GameObject("Aim Point").transform;
    }

    private void Start() 
    {
        mainCamera = Camera.main;

        if (TryGetComponent(out PlayerInputController input))
        {
            input.Actions.Aim.started += (context) => {AimStarted();};
            input.Actions.Aim.canceled += (context) => {AimCanceled();};

            input.Actions.Fire.started += (context) => {Fire();};
        }
    }

    private void Update() 
    {
        if (isAiming && Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Aim();
        }
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
        // 애니메이션으로 처리하지 않을 것
        DummyWeapon weapon = equip.CurrentEquip;
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.projectile.data.type);
        bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward);
    }

    void Aim()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit , float.PositiveInfinity, aimLayerMask))
        {
            aimPoint.position = hit.point;
        }
        else
        {
            aimPoint.position = transform.position + transform.forward + Vector3.up * 1.5f;
        }
    }

}
