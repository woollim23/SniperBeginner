using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
    }
    
}