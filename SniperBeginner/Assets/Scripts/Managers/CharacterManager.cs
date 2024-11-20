using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] Player playerPrefab;
    [SerializeField] public List<GameObject> enemies;
    public Player Player { get; set; }

    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Transform[] SpawnPoints;

    private void Start() 
    {
        if(!Player)
            Instantiate(playerPrefab);

        InstaiateEnemy();
    }

    public void Initialize()
    {
        Player = Instantiate(playerPrefab);
        Player.Initialize(null); // TODO : 로드한 데이터 받아오기

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
                enemies.Add(newEnemy.gameObject);
            }
        }
    }

}