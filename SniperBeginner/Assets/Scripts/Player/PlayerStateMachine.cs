using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }
    public PlayerSetting Setting { get; private set; }

    public Camera camera;
    
    public IState CurrentIdle { get; set; }
    // Base Layer    
    public IState StandState { get; private set; }
    public IState CrouchState { get; private set; }
    public IState CrawlState { get; private set; }
    // Upper Layer
    public IState AimState { get; private set; }
    public IState FireState { get; private set; }


    public PlayerStateMachine(Player player)
    {
        Player = player;

        camera = Camera.main;

        // Idle 계열
        StandState = new PlayerStandState(this);
        CrouchState = new PlayerCrouchState(this);
        CrawlState = new PlayerCrawlState(this);

        // Fire 
        AimState = new PlayerAimState(this);
        FireState = new PlayerFireState(this);

        Setting = Player.setting;
    }
}