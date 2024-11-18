
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animation.InAir(true);
    }

    public override void Exit()
    {
        base.Exit();
        animation.InAir(false);
    }

    public override void Update()
    {
        base.Update();

        Fall();
    }

    protected virtual void Fall()
    {
        controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }
}