using UnityEngine.InputSystem;

/// <summary>
/// 일반 상태 - 쪼그려 앉은 상태
/// </summary>
public class PlayerCrouchState : PlayerIdleState
{
    public PlayerCrouchState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        animation.Animator.SetBool(animation.CrouchParamHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        animation.Animator.SetBool(animation.CrouchParamHash, false);
    }

    protected override void OnPose(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.CrawlState);
    }

    protected override void Move()
    {
        base.Move();
    }
}
