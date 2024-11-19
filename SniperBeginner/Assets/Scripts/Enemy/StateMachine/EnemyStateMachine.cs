using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; }
    public float FieldOfView { get; private set; }

    public GameObject Target { get; private set; }
    public EnemyIdleState IdleState { get; }
    public EnemyWanderState WanderState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;
        Target = GameObject.FindGameObjectWithTag("Player");

        IdleState = new EnemyIdleState(this);
        WanderState = new EnemyWanderState(this);
        AttackState = new EnemyAttackState(this);

        Enemy.agent.speed = Enemy.Data.GroundData.BaseSpeed;

        // -- º¸·ù
        MovementSpeed = Enemy.Data.GroundData.BaseSpeed;
        RotationDamping = Enemy.Data.GroundData.BaseRotationDamping;
        FieldOfView = Enemy.Data.GroundData.BaseFiledOfView;
    }
}
