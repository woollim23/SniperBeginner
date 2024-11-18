using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    CharacterController controller;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        controller = stateMachine.Player.Controller;
    }

    public override void Enter()
    {
        base.Enter();
        
        stateMachine.Player.ForceReceiver.AddForce(Vector3.up * stateMachine.Player.setting.JumpPower);

        // 애니메이션 적용
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(controller.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.StandState);
        }
    }
}