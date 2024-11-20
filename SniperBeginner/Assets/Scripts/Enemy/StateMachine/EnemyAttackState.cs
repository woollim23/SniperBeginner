using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyAppliedForce;
    //private bool alreadyAppliedDealing;

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("Attack");
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);

        alreadyAppliedForce = false;
        //alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);
    }

    public override void Update()
    {
        base.Update();

        // 애니메이션 진행도를 0.0 ~ 1.0
        //float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (!IsInChasingRange()) // 플레이어가 범위 밖이면
        {
            stateMachine.ChangeState(stateMachine.IdleState); // 아이들 상태로 변경
            return;
        }

    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Enemy.ForceReceiver.Reset();

        stateMachine.Enemy.ForceReceiver.AddForce(Vector3.forward * stateMachine.Enemy.Data.Force);
    }
}
