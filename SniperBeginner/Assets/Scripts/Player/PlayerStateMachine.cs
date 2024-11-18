using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }
    public PlayerSetting Setting { get; private set; }
    
    public IState CurrentIdle { get; set; }
    // Idle 계열    
    public PlayerStandState StandState { get; private set; }
    public PlayerCrouchState CrouchState { get; private set; }
    public PlayerCrawlState CrawlState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        Player = player;

        // Idle 계열
        StandState = new PlayerStandState(this);
        CrouchState = new PlayerCrouchState(this);
        CrawlState = new PlayerCrawlState(this);

        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);

        Setting = Player.setting;
    }
}