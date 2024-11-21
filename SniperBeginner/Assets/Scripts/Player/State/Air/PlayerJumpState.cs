using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        stateMachine.Player.ForceReceiver.AddForce(Vector3.up * stateMachine.Player.setting.jumpPower);

        // 애니메이션 적용
        animation.Jump();

        stateMachine.ChangeState(stateMachine.FallState);
    }

}