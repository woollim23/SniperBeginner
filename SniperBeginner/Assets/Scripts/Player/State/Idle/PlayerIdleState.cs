using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 일반 상태
/// </summary>
public class PlayerIdleState : PlayerBaseState
{
    protected bool IsRun { get; set; } = false;
    protected Vector2 movement = Vector2.zero;

    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        Initialize();
    }

    protected override void AddPlayerInput()
    {
        stateMachine.CurrentIdle = this;

        base.AddPlayerInput();
        stateMachine.Player.Actions.Pose.started += OnPose;

        stateMachine.Player.Actions.Run.started += OnRun;
        stateMachine.Player.Actions.Run.canceled += OnRun;
    }

    protected override void RemovePlayerInput()
    {
        base.RemovePlayerInput();
        stateMachine.Player.Actions.Pose.started -= OnPose;
        
        stateMachine.Player.Actions.Run.started -= OnRun;
        stateMachine.Player.Actions.Run.canceled -= OnRun;
    }

    public override void Update()
    {
        base.Update();
        
        movement = Vector2.Lerp(movement, moveInput, stateMachine.Setting.MovementInputSmoothness);

        Move();
    }


    // 자세 전환 - 자식에서 구현
    protected virtual void OnPose(InputAction.CallbackContext context) {}

    protected virtual void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
            IsRun = true;
        else if (context.canceled)
            IsRun = false;

        animation.Animator.SetBool(animation.data.RunParamHash, IsRun);
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        // 스테이트 변경
        if (stateMachine.Player.Controller.isGrounded)
            stateMachine.ChangeState(stateMachine.JumpState);
    }

    void Initialize()
    {
        movement = Vector2.zero;
        animation.Move(Vector2.zero);
    }

    protected virtual void Move()
    {
        animation.Move(movement);
    }

}
