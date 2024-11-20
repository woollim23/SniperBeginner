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

        // �ִϸ��̼� ���൵�� 0.0 ~ 1.0
        //float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (!IsInAttackRange()) // �÷��̾ ���� ���̸�
        {
            if (IsInChasingRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState); // ���̵� ���·� ����
            }
        }

        // TODO : �÷��̾ ������ ���ߵ��� or �÷��̾� ���� �̺�Ʈ �޾Ƽ� ���� ��ȭ
    }
}
