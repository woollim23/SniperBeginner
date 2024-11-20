using System.Collections;
using System.Collections.Generic;
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

        stateMachine.Enemy.OnEnemyGunFire?.Invoke();

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

    public IEnumerator WaitForAnimationToEnd()
    {
        float animationLength = stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);
        stateMachine.ChangeState(stateMachine.AimingState);
    }
}
