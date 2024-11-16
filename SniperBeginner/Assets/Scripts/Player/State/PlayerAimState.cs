using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerBaseState
{
    public PlayerAimState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        animation.Move(Vector2.zero);
        animation.Aiming(true);
    }

    public override void Exit()
    {
        base.Exit();
        animation.Aiming(false);
    }

    public override void Update()
    {
        base.Update();
    }

}
