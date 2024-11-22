public class EnemyWanderState : EnemyBaseState
{
    public EnemyWanderState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);

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
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
        else if(stateMachine.Enemy.Agent.velocity.magnitude <= 0.1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}