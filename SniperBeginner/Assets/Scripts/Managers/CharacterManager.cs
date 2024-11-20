using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] Player playerPrefab;
    public Player Player { get; set; }

    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Transform[] SpawnPoints;

    public void Initialize()
    {
        Player = Instantiate(playerPrefab);

        // 추후에 적들도 생성
    }

    public void InstaiateEnemy()
    {
        Enemy e = Instantiate(enemyPrefab);
        // e.Agent.Warp(Position);
        
    }

}