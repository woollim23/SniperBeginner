using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] Player playerPrefab;
    [SerializeField] public List<Enemy> enemies;
    public Player Player { get; set; }

    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Transform[] SpawnPoints;

    public void Initialize()
    {
        InstantiatePlayer();
        InstaiateEnemy();
    }

    public void InstaiateEnemy()
    {
        if (false)
        {
            // TODO : ????? ??????? ???? ?? ???? ??? ?????
            // ??????????? ????? ??????? ??? ????
            for (int i = 0; i < SpawnPoints.Length; i++)
            {
                Enemy newEnemy = Instantiate(enemyPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);

                // ???? ?????? ????? ???????? ????
                if (newEnemy.Health <= 0)
                    GameManager.Instance.Score++;
            }
        }
        else
        {
            for (int i = 0; i < SpawnPoints.Length; i++)
            {
                Enemy newEnemy = Instantiate(enemyPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
                enemies.Add(newEnemy);
            }
        }
    }

    public void InstantiatePlayer()
    {   
        Player = Instantiate(playerPrefab);

        if (DataManager.Instance.IsLoadedGame)
        {
            Player.Initialize(null);
        }
        else
        {
            Player.Initialize(null);
        }
    }
}