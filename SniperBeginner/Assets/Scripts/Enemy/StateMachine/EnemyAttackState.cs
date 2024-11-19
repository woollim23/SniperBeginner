using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyAppliedDealing;

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.AimingParameterName);
        StartAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);

        alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.AimingParameterName);
        StopAnimation(stateMachine.Enemy.AnimationData.FireParameterHash);
    }

    public override void Update()
    {
        base.Update();

        // �ִϸ��̼� ���൵�� 0.0 ~ 1.0
        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f) // ���� �ִϸ��̼��� ���� �� ����
        {
            // ���� �������� ����X, �ִϸ��̼� ���൵ �ð��� Dealing_Start_TransitionTime �̻��϶�
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                // ���⸦ Ȱ��ȭ �ϰų� ���ݷ��� ����

                //stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
                //stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true; // ������ ����o ���� ����
            }

            // ������ ����o, �ִϸ��̼��� Dealing_End_TransitionTime �̻��϶�
            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                // ���⸦ ��Ȱ��ȭ�ϴ� �ڵ带 ����

                //stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }
        }
        else // ���� �ִϸ��̼��� ���� �� ���� 
        {
            if (IsInChasingRange()) // �÷��̾ ���� ���� ���̸�
            {
                stateMachine.ChangeState(stateMachine.ChasingState); // ���� ���·� ����
                return;
            }
            else // ���� ���̸�
            {
                stateMachine.ChangeState(stateMachine.IdleState); // ���̵� ���·� ����
                return;
            }
        }

    }
}
