using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderState : EnemyBaseState
{
    public EnemyWanderState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("Wander");
        
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);

        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
        else if(stateMachine.Enemy.Agent.velocity.magnitude <= 0.1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }
}