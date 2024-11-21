using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }
    public PlayerSetting Setting { get; private set; }
    public Vector2 Movement {get; set;}
    public Vector2 MoveInput {get; set;}
    
    public IState CurrentIdle { get; set; }
    // Idle 계열    
    public PlayerStandState StandState { get; private set; }
    public PlayerCrouchState CrouchState { get; private set; }
    // Air 계열
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }


    public PlayerStateMachine(Player player)
    {
        Player = player;
        Setting = player.setting;

        StandState = new PlayerStandState(this);
        CrouchState = new PlayerCrouchState(this);

        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        DeadState = new PlayerDeadState(this);
    }



    void SetDead()
    {
        ChangeState(DeadState);
    }
}
