using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);

        stateMachine.Enemy.Agent.isStopped = true;

        Fire();
        stateMachine.Enemy.StartCoroutine(WaitForAnimationToEnd());
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);

        stateMachine.Enemy.Agent.isStopped = false;
        stateMachine.LastAttackTime = Time.time;
    }

    void Fire()
    {
        Weapon weapon = stateMachine.Enemy.Weapon;
        Debug.Log(weapon);
        Debug.Log(weapon.weaponData);
        Debug.Log(weapon.weaponData.projectile);
        Debug.Log(weapon.weaponData.projectile.data);
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.weaponData.projectile.data.type);

        Ray ray = new Ray(weapon.firePoint.position, weapon.firePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, weapon.weaponData.range))
        {
            if (hitInfo.collider.TryGetComponent(out IDamagable damagable))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    // Player라면 데미지 처리
                    damagable.TakeDamage(weapon.weaponData.damage);
                }
            }
        }

        bullet.Fire(weapon.firePoint.position, weapon.firePoint.forward);

        stateMachine.Enemy.OnEnemyGunFire?.Invoke();
        SoundManager.Instance.PlaySound(weapon.weaponData.fireSound);
    }

    public IEnumerator WaitForAnimationToEnd()
    {
        float animationLength = stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);
        stateMachine.ChangeState(stateMachine.AimingState);
    }
}
