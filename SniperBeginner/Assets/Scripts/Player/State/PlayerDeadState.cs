public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.Player.Animation.AnimationSwitch(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

}