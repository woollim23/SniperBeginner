using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    //private bool alreadyAppliedForce;
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

        stateMachine.Enemy.Agent.isStopped = true;
        //alreadyAppliedForce = false;
        //alreadyAppliedDealing = false;
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
        Rotate(CharacterManager.Instance.Player.transform.position - stateMachine.Enemy.transform.position);

        // 애니메이션 진행도를 0.0 ~ 1.0
        //float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
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

        // TODO : 플레이어가 죽으면 멈추도록 or 플레이어 죽음 이벤트 받아서 상태 변화
    }
}
