using UnityEngine.InputSystem;

/// <summary>
/// 일반 상태 - 서 있는 상태
/// </summary>
public class PlayerStandState : PlayerIdleState
{
    public PlayerStandState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        animation.Animator.SetBool(animation.data.StandParamHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        animation.Animator.SetBool(animation.data.StandParamHash, false);
    }

    protected override void OnPose(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.CrouchState);
    }
}
