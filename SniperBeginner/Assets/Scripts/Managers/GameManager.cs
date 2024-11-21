using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameData GameData { get; private set; }
    public int Score { get; set; } = 0;
    public bool isGameOver { get; private set; }

    public event Action onChangeScore;
    
    public void Awake() 
    {
        // 게임 내에서만 쓰는 매니저들 Initialize
        CharacterManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        CameraManager.Instance.Initialize();
    }

    private void Start()
    {
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
        Score++;
        onChangeScore?.Invoke();

        if (Score == CharacterManager.Instance.enemies.Count)
            GameClear();
    }

    public void SaveGame()
    {
        if (GameData == null)
        {
            GameData = new GameData
            {
                playerData = new PlayerData()
            };
        }
        Player player = CharacterManager.Instance.Player; // Player 인스턴스 가져오기
        if (player != null)
        {
            GameData.playerData.Position = player.transform.position; // 플레이어 위치 저장
            GameData.playerData.Health = player.Condition.Health; // 플레이어 체력 저장
            // 현재 총기 저장
            // 현재 총알 수 저장
        }

        DataManager.Instance.SaveGameData(GameData);
    }


    public void LoadGame()
    {
        DataManager.Instance.LoadGameData();
        GameData = DataManager.Instance.CurrentGameData;

    }

}
