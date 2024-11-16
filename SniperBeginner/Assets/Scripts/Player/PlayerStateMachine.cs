using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }
    public PlayerSetting Setting { get; private set; }

    public Camera camera;
    
    public PlayerIdleState StandState { get; private set; }
    public PlayerIdleState CrouchState { get; private set; }
    public PlayerIdleState CrawlState { get; private set; }

    
    public IState AimState { get; private set; }
    public IState ShotState { get; private set; }


    public PlayerStateMachine(Player player)
    {
        Player = player;

        camera = Camera.main;

        StandState = new PlayerStandState(this);
        CrouchState = new PlayerCrouchState(this);
        CrawlState = new PlayerCrawlState(this);

        AimState = new PlayerAimState(this);
        ShotState = new PlayerShotState(this);

        Setting = Player.setting;
    }
    
}