using UnityEngine.InputSystem;

/// <summary>
/// 일반 상태
/// </summary>
public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    protected override void AddPlayerInput()
    {
        base.AddPlayerInput();
        stateMachine.Player.Input.Actions.Pose.started += OnPose;
    }

    protected override void RemovePlayerInput()
    {
        base.RemovePlayerInput();
        stateMachine.Player.Input.Actions.Pose.started -= OnPose;
    }

    public override void Update()
    {
        base.Update();
        stateMachine.Player.Animation.Move(Movement);
    }

    // 자세 전환 - 자식에서 구현
    protected virtual void OnPose(InputAction.CallbackContext context) {}
}
