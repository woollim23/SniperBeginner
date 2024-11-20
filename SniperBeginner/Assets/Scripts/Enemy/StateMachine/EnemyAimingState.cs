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
        Debug.Log("aiming");
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
        if(stateMachine.Enemy.Health > 0)
            Rotate(CharacterManager.Instance.Player.transform.position - stateMachine.Enemy.transform.position);

        if (!IsInAttackRange()) // 플레이어가 범위 밖이면
        {
            if (IsInChasingRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState); // 아이들 상태로 변경
            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }

        // TODO : 플레이어가 죽으면 멈추도록 or 플레이어 죽음 이벤트 받아서 상태 변화
    }
}
