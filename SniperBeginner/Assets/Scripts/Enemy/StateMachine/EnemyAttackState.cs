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

        // 애니메이션 진행도를 0.0 ~ 1.0
        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f) // 공격 애니메이션이 진행 중 동작
        {
            // 아직 데미지를 적용X, 애니메이션 진행도 시간이 Dealing_Start_TransitionTime 이상일때
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                // 무기를 활성화 하거나 공격력을 설정

                //stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
                //stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true; // 데미지 적용o 으로 변경
            }

            // 데미지 적용o, 애니메이션이 Dealing_End_TransitionTime 이상일때
            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                // 무기를 비활성화하는 코드를 실행

                //stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }
        }
        else // 공격 애니메이션이 끝난 후 실행 
        {
            if (IsInChasingRange()) // 플레이어가 추적 범위 안이면
            {
                stateMachine.ChangeState(stateMachine.ChasingState); // 추적 상태로 변경
                return;
            }
            else // 범위 밖이면
            {
                stateMachine.ChangeState(stateMachine.IdleState); // 아이들 상태로 변경
                return;
            }
        }

    }
}
