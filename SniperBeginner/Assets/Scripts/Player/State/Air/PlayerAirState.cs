
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
        Transform player = stateMachine.Player.transform;
        float speed = stateMachine.Setting.speedMultiplyOnJump * Time.deltaTime;
        Vector3 motion = player.right * stateMachine.Movement.x * speed + player.forward * stateMachine.Movement.y * speed;
        motion += stateMachine.Player.ForceReceiver.Movement * Time.deltaTime;

        controller.Move(motion);
    }
}