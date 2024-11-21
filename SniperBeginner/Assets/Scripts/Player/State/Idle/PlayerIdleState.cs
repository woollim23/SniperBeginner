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

        animation.InGround(true);
    }

    public override void Exit()
    {
        base.Exit();

        animation.InGround(false);
    }

    public override void Update()
    {
        base.Update();
        
        movement = Vector2.Lerp(movement, moveInput, stateMachine.Setting.movementInputSmoothness);
        Move();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!controller.isGrounded && 
            controller.velocity.y < -stateMachine.Setting.fallThreshold)
        {
            stateMachine.ChangeState(stateMachine.FallState);
        }
    }


    protected override void AddPlayerInput()
    {
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
        if (controller.isGrounded)
            stateMachine.ChangeState(stateMachine.JumpState);
    }

    void Initialize()
    {
        // movement = Vector2.zero;
        moveInput = Vector2.zero;
        animation.Move(Vector2.zero);
    }

    protected virtual void Move()
    {
        animation.Move(movement);
    }

}
