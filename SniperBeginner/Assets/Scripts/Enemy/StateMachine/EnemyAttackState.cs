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

        // �ִϸ��̼� ���൵�� 0.0 ~ 1.0
        //float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (!IsInChasingRange()) // �÷��̾ ���� ���̸�
        {
            stateMachine.ChangeState(stateMachine.IdleState); // ���̵� ���·� ����
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
