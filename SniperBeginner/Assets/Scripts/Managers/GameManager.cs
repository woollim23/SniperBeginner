using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestory<GameManager>
{
    [SerializeField] public List<GameObject> enemies;

    public GameData GameData { get; private set; }

    public event Action onChangeScore;

    public bool isGameOver;


    private void Start()
    {
        if (GameData == null)
        {
            Debug.Log("GameData가 null입니다. 기본값을 생성합니다.");
            LoadGame();
        }
    }

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

    public void SaveGame()
    {
        
        Player player = CharacterManager.Instance.Player; // Player 인스턴스 가져오기
        if (player != null)
        {
            GameData.playerData.Position = player.transform.position; // 플레이어 위치 저장
            GameData.playerData.Health = player.Condition.Health; // 플레이어 체력 저장
            // 플레이어 장비 저장
            // 플레이어 총알 저장
        }

        DataManager.Instance.SaveGameData(GameData);
    }

    public void LoadGame()
    {
        DataManager.Instance.LoadGameData();
        if (GameData == null)
        {
            Player player = CharacterManager.Instance.Player;

            GameData = new GameData
            {
                // 새 데이터는 현재 값을 받아오도록
                playerData = new PlayerData()
                {
                    Position = player.transform.position,
                    Health = player.Condition.Health,
                }
            };
        }

    }
}
