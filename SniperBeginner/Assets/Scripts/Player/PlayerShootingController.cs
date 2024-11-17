using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    PlayerAnimationController anim;
    PlayerEquipment equip;
    


    private void Awake() 
    {
        anim = GetComponent<PlayerAnimationController>();
        equip = GetComponent<PlayerEquipment>();
    }

    private void Start() 
    {
        PlayerInputController input = GetComponent<PlayerInputController>();

        input.Actions.Aim.started += (context) => {AimStarted();};
        input.Actions.Aim.canceled += (context) => {AimCanceled();};

        input.Actions.Fire.started += (context) => {Fire();};
    }


    void AimStarted()
    {
        anim.Aiming(true);
    }

    void AimCanceled()
    {
        anim.Aiming(false);
    }

    void Fire()
    {
        // 애니메이션으로 처리하지 않을 것
        DummyWeapon weapon = equip.CurrentEquip;
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.projectile.data.type);
        bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward);
    }

}
