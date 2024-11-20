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
            // TODO : ����� ����Ʈ�� ī��Ʈ �� ���� �ݺ� �ǵ���
            // ���嵥���Ϳ��� ��ġ�� �ҷ����� ü�� ����
            for (int i = 0; i < SpawnPoints.Length; i++)
            {
                Enemy newEnemy = Instantiate(enemyPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);

                // óġ�� ���ʹ̸� ���ھ �������� ����
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