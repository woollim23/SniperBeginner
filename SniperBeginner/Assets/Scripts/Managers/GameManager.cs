using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestory<GameManager>
{
    [SerializeField] public List<GameObject> enemies;

    public event Action onChangeScore;

    public bool isGameOver;


    public void GameStartInit()
    {
        // 게임 시작 초기화 함수
        isGameOver = false;
    }

    public void GameClear()
    {
        
    }

    public void CountDeadEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponentInChildren<Enemy>().Health <= 0)
                enemies.Remove(enemies[i]);
        }

        onChangeScore?.Invoke();
    }
}
