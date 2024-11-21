using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 일반 상태 - 쪼그려 앉은 상태
/// </summary>
public class PlayerCrouchState : PlayerIdleState
{
    public PlayerCrouchState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        animation.Animator.SetBool(animation.data.CrouchParamHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        animation.Animator.SetBool(animation.data.CrouchParamHash, false);
    }

    protected override void OnPose(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.StandState);
    }

    protected override void Move()
    {
        base.Move();

        Transform player = stateMachine.Player.transform;
        float speed = stateMachine.Setting.crouchSpeed * Time.deltaTime;

        Vector3 motion = player.right * stateMachine.Movement.x * speed + player.forward * stateMachine.Movement.y * speed;
        motion += stateMachine.Player.ForceReceiver.Movement * Time.deltaTime;
        
        stateMachine.Player.Controller.Move(motion);
    }
}
