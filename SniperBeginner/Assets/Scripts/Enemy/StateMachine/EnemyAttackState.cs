using System.Collections;
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

        stateMachine.Enemy.StartCoroutine(WaitForAnimationToEnd());
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);

        stateMachine.Enemy.Agent.isStopped = false;
    }

    void Fire()
    {
        Weapon weapon = stateMachine.Enemy.Weapon;
        Projectile bullet = ObjectPoolManager.Instance.Get(weapon.weaponData.projectile.data.type);

        Vector3 firePoint = weapon.firePoint.transform.position;
        Vector3 targetPos = stateMachine.Target.transform.position + Vector3.up * (1f + Random.Range(-0.3f, 0.5f));

        Vector3 dir = targetPos - firePoint;
        bullet.Fire(firePoint, dir, stateMachine.Enemy.Data.Damage);

        stateMachine.LastAttackTime = Time.time;

        ParticleManager.Instance.SpawnMuzzleFlash(weapon.firePoint);
        SoundManager.Instance.PlaySound(weapon.weaponData.fireSound);
    }

    public IEnumerator WaitForAnimationToEnd()
    {
        yield return new WaitForSeconds(0.2f);
        Fire();

        float animationLength = stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        stateMachine.ChangeState(stateMachine.AimingState);
    }
}
