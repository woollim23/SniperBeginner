using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimationController animation;
    protected Vector2 moveInput;


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
    }


    protected virtual void AddPlayerInput()
    {
        stateMachine.Player.Input.Actions.Move.performed += OnMove;
        stateMachine.Player.Input.Actions.Move.canceled += OnMove;

        stateMachine.Player.Input.Actions.Aim.started += OnAim;
        stateMachine.Player.Input.Actions.Aim.canceled += OnAim;

        stateMachine.Player.Input.Actions.Fire.started += OnFire;
    }

    protected virtual void RemovePlayerInput()
    {
        stateMachine.Player.Input.Actions.Move.performed -= OnMove;
        stateMachine.Player.Input.Actions.Move.canceled -= OnMove;

        stateMachine.Player.Input.Actions.Aim.started -= OnAim;
        stateMachine.Player.Input.Actions.Aim.canceled -= OnAim;

        stateMachine.Player.Input.Actions.Fire.started -= OnFire;
    }

    protected virtual void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    protected virtual void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
            stateMachine.ChangeState(stateMachine.AimState);
        else if (context.canceled)
            stateMachine.ChangeState(stateMachine.CurrentIdle);
    }

    protected virtual void OnFire(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.FireState);
    }
}