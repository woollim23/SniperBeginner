using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; private set; }

    public float BaseSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; }
    public float RotationDamping { get; private set; }
    public float FieldOfView { get; private set; }

    public GameObject Target { get; private set; }
    public EnemyIdleState IdleState { get; }
    public EnemyWanderState WanderState { get; private set; }
    public EnemyWarningState WarningState { get; private set; }
    public EnemyChasingState ChasingState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;
        Target = GameObject.FindGameObjectWithTag("Player");

        IdleState = new EnemyIdleState(this);
        WanderState = new EnemyWanderState(this);
        WarningState = new EnemyWarningState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);

        BaseSpeed = Enemy.Data.GroundData.BaseSpeed;
        MovementSpeedModifier = Enemy.Data.GroundData.WalkSpeedModifier;
        RotationDamping = Enemy.Data.GroundData.BaseRotationDamping;
        FieldOfView = Enemy.Data.GroundData.BaseFiledOfView;
    }

    protected void ChangeWarningState()
    {
        
    }
}
