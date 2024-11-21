using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; private set; }
    public float RotationDamping { get; private set; }
    public float RateAttackTime { get; private set; }
    public float LastAttackTime { get; set; }
    public GameObject Target { get; private set; }
    public EnemyIdleState IdleState { get; }
    public EnemyWanderState WanderState { get; private set; }
    public EnemyChasingState ChasingState { get; private set; }
    public EnemyAimingState AimingState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;
        Target = CharacterManager.Instance.Player.gameObject;//GameObject.FindGameObjectWithTag("Player");

        IdleState = new EnemyIdleState(this);
        WanderState = new EnemyWanderState(this);
        ChasingState = new EnemyChasingState(this);
        AimingState = new EnemyAimingState(this);
        AttackState = new EnemyAttackState(this);

        RotationDamping = Enemy.Data.GroundData.BaseRotationDamping;
        RateAttackTime = Enemy.Data.RateAttackTime;
    }
}
