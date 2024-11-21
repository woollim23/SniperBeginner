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

        if (DataManager.Instance.IsLoadedGame)
        {
            foreach (var enemy in GameManager.Instance.GameData.enemyData)
            {
                Enemy newEnemy = Instantiate(enemyPrefab, enemy.Position, Quaternion.Euler(0, 90, 0));

                newEnemy.Health = enemy.Health;
                enemies.Add(newEnemy);
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
        if(Player != null)
        {
            Destroy(Player.gameObject);
        }

        Player = Instantiate(playerPrefab);

        if (DataManager.Instance.IsLoadedGame)
        {
            Player.Initialize(DataManager.Instance.CurrentGameData.playerData);
        }
        else
        {
            Player.Initialize(null);
        }
    }
}