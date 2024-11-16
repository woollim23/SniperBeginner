using UnityEngine;
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

        stateMachine.Player.Input.Actions.Run.started += OnRunStarted;
        stateMachine.Player.Input.Actions.Run.canceled += OnRunCanceled;
    }

    protected override void RemovePlayerInput()
    {
        base.RemovePlayerInput();
        stateMachine.Player.Input.Actions.Pose.started -= OnPose;
        
        stateMachine.Player.Input.Actions.Run.started -= OnRunStarted;
        stateMachine.Player.Input.Actions.Run.canceled -= OnRunCanceled;
    }

    public override void Update()
    {
        base.Update();
        Move();
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        IsRun = true;
        animation.Animator.SetBool(animation.data.RunParamHash, IsRun);
    }
    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        IsRun = false;
        animation.Animator.SetBool(animation.data.RunParamHash, IsRun);
    }

    // 자세 전환 - 자식에서 구현
    protected virtual void OnPose(InputAction.CallbackContext context) {}

    public void Move()
    {
        animation.Move(Movement);

        float speed = (IsRun ? stateMachine.Setting.RunSpeed : stateMachine.Setting.WalkSpeed) * Time.deltaTime;
        stateMachine.Player.Controller.Move(new Vector3(Movement.x * speed, 0f, Movement.y * speed));
    }
}
