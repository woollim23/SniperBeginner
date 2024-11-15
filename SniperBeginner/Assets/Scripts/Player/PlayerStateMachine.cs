using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }

    public Camera camera;
    
    public Vector2 MovementInput { get; private set; }
    public float WalkSpeed { get; private set; }
    public float RunSpeed { get; private set; }

    
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


        WalkSpeed = Player.WalkSpeed;
        RunSpeed = Player.RunSpeed;

    }
    
}