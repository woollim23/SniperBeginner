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
        Debug.Log("Attack");
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);

        stateMachine.Enemy.Agent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);

        stateMachine.Enemy.Agent.isStopped = false;
    }

    public override void Update()
    {
        
        Debug.Log("발사");

        // 애니메이션 진행도를 0.0 ~ 1.0
        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Fire");
        

        // TODO : 플레이어가 죽으면 멈추도록 or 플레이어 죽음 이벤트 받아서 상태 변화
    }
}
