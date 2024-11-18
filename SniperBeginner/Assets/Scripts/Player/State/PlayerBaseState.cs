using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState : IState
{
    protected CharacterController controller;
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimationController animation;
    protected Vector2 moveInput;


    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        animation = stateMachine.Player.Animation;
        controller = stateMachine.Player.Controller;
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
    }
    public virtual void FixedUpdate() 
    {
    }

    protected virtual void AddPlayerInput()
    {
        stateMachine.Player.Actions.Move.performed += OnMove;
        stateMachine.Player.Actions.Move.canceled += OnMove;

        stateMachine.Player.Actions.Jump.started += OnJump;
    }

    protected virtual void RemovePlayerInput()
    {
        stateMachine.Player.Actions.Move.performed -= OnMove;
        stateMachine.Player.Actions.Move.canceled -= OnMove;

        stateMachine.Player.Actions.Jump.started -= OnJump;
    }

    protected virtual void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    protected virtual void OnJump(InputAction.CallbackContext context)
    {
    }

}