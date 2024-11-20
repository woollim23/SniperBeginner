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
        animation.Animator.SetBool(animation.data.StandParamHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        animation.Animator.SetBool(animation.data.StandParamHash, false);
    }

    protected override void OnPose(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.CrouchState);
    }

    protected override void Move()
    {
        base.Move();
        
        Transform player = stateMachine.Player.transform;
        float speed = (IsRun ? stateMachine.Setting.runSpeed : stateMachine.Setting.walkSpeed) * Time.deltaTime;

        Vector3 motion = player.right * movement.x * speed + player.forward * movement.y * speed;
        motion += stateMachine.Player.ForceReceiver.Movement * Time.deltaTime;
        
        stateMachine.Player.Controller.Move(motion);
    }
}
