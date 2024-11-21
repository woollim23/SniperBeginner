using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

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
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.weaponData.projectile.data.type);

        Transform firePoint = weapon.firePoint.transform;
        bullet.Fire(firePoint.position, firePoint.forward, stateMachine.Enemy.Data.Damage);

        ParticleManager.Instance.SpawnMuzzleFlash(weapon.firePoint);
        SoundManager.Instance.PlaySound(weapon.weaponData.fireSound);
    }

    public IEnumerator WaitForAnimationToEnd()
    {
        float animationLength = stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);
        stateMachine.ChangeState(stateMachine.AimingState);
    }
}
