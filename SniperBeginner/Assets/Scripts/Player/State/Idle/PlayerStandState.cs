using UnityEngine;
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
        animation.Animator.SetBool(animation.StandParamHash, true);

        Debug.Log("Stand");
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override void OnPose(InputAction.CallbackContext context)
    {
        animation.Animator.SetBool(animation.StandParamHash, false);
        stateMachine.ChangeState(stateMachine.CrouchState);
    }

    protected override void Move()
    {
        base.Move();

        float speed = (IsRun ? stateMachine.Setting.RunSpeed : stateMachine.Setting.WalkSpeed) * Time.deltaTime;
        stateMachine.Player.Controller.Move(new Vector3(movement.x * speed, 0f, movement.y * speed));
    }
}
