using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimingState : EnemyBaseState
{
    public EnemyAimingState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);

        stateMachine.Enemy.Agent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);

        stateMachine.Enemy.Agent.isStopped = false;
    }

    public override void Update()
    {
        if (stateMachine.Enemy.Health > 0)
            Rotate(stateMachine.Target.transform.position - stateMachine.Enemy.transform.position);

        if (!IsInAttackRange()) // 플레이어가 공격 범위 밖이면
        {
            if (IsInChasingRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState); // 추적 상태
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState); // 아이들 상태
                return;
            }
        }
        else if(stateMachine.RateAttackTime <= Time.time - stateMachine.LastAttackTime && !stateMachine.Enemy.isDeadEnemy)
        {
            stateMachine.ChangeState(stateMachine.AttackState); // 공격 상태
            return;
        }
    }
}
