using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>
{

    public GameData GameData { get; set; }
    private SaveLoadManager saveLoadManager;
    public int Score { get; set; } = 0;
    public bool isGameOver { get; private set; }

    public event Action onChangeScore;
    
    public void Awake() 
    {
        saveLoadManager = new SaveLoadManager();

        // 게임 내에서만 쓰는 매니저들 Initialize
        CharacterManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        CameraManager.Instance.Initialize();
    }

    private void Start()
    {
        SoundManager.Instance.PlayBackgroundMusic(SoundManager.Instance.gameBGM);
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
        saveLoadManager.SaveGame(this);
    }


    public void LoadGame()
    {
        saveLoadManager.LoadGame(this);
    }

}
