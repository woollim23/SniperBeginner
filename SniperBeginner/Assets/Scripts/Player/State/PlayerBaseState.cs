using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimationController animation;
    
    protected bool IsRun { get; set; } = false;
    protected Vector2 Movement { get; set; } = Vector2.zero;
    Vector2 MoveInput { get; set; }

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        animation = stateMachine.Player.Animation;
    }
    

    public virtual void Enter()
    {
        AddPlayerInput();
    }

    public virtual void Exit()
    {
        RemovePlayerInput();
    }

    public virtual void Update() 
    {
        Movement = Vector2.Lerp(Movement, MoveInput, stateMachine.Setting.MovementInputSmoothness);
    }


    protected virtual void AddPlayerInput()
    {
        stateMachine.Player.Input.Actions.Move.performed += OnMove;
        stateMachine.Player.Input.Actions.Move.canceled += OnMove;
    }

    protected virtual void RemovePlayerInput()
    {
        stateMachine.Player.Input.Actions.Move.performed -= OnMove;
        stateMachine.Player.Input.Actions.Move.canceled -= OnMove;
    }

    protected virtual void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
}