using UnityEngine.InputSystem;

/// <summary>
/// 일반 상태 - 엎드린 상태
/// </summary>
public class PlayerCrawlState : PlayerIdleState
{
    public PlayerCrawlState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        animation.Animator.SetBool(animation.data.CrawlParamHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        animation.Animator.SetBool(animation.data.CrawlParamHash, false);
    }

    protected override void OnPose(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.StandState);
    }

    protected override void Move()
    {
        base.Move();
    }
}
