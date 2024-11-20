using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] Player playerPrefab;
    public Player Player { get; set; }

    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] Enemy enemyPrefab;

    private void Start() 
    {
        if(!Player)
            Instantiate(playerPrefab);
    }

    public void InstaiateEnemy()
    {
        Enemy e = Instantiate(enemyPrefab);
        // e.Agent.Warp(Position);
        
    }

}