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
        else
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }

        // TODO : �÷��̾ ������ ���ߵ��� or �÷��̾� ���� �̺�Ʈ �޾Ƽ� ���� ��ȭ
    }
}
