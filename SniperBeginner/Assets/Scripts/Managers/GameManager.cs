using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestory<GameManager>
{
    [SerializeField] private List<GameObject> enemies;
    
    public bool isGameOver;

    public void GameStartInit()
    {
        // 게임 시작 초기화 함수
        isGameOver = false;
    }

    public void GameClear()
    {
        // 게임 클리어 확인 함수
    }

    public void CountDeadEnemy()
    {
        // 적 처치 카운팅 함수
    }
}
