using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] Player playerPrefab;
    [SerializeField] public List<GameObject> enemies;
    public Player Player { get; set; }

    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] Enemy enemyPrefab;

    private void Start() 
    {
        if (!Player) Instantiate(playerPrefab);
        InstaiateEnemy();
    }

    public void InstaiateEnemy()
    {
        if (false)
        {
            // TODO : 저장된 리스트의 카운트 수 만들 반복 되도록
            // 저장데이터에서 위치값 불러오고 체력 적용
            for (int i = 0; i < SpawnPoints.Length; i++)
            {
                Enemy newEnemy = Instantiate(enemyPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);

                // 처치된 에너미면 스코어가 오르도록 수정
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