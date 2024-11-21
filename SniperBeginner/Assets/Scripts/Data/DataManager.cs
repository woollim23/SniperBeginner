using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : SingletonDontDestory<DataManager>
{
    public bool IsLoadedGame = false;
    public bool existData = false;
    public GameData CurrentGameData { get; private set; } // 현재 게임 데이터

    private static string gameDataPath => Application.dataPath + "/GameData.json"; 
    // TODO:: 문제 없으면 Application.persistentDataPath로 바꾸기

    public void SaveGameData(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(gameDataPath, json);
        CurrentGameData = data;
    }

    public void LoadGameData()
    {
        if (!File.Exists(gameDataPath)) // 저장 파일 확인
        {
            Debug.Log("저장 파일 없음");
            CurrentGameData = new GameData
            {
                playerData = new PlayerData(),
                enemyData = new List<EnemyData>()

            };
            existData = false;
        }
        else
        {
            string json = File.ReadAllText(gameDataPath);
            CurrentGameData = JsonUtility.FromJson<GameData>(json);
            existData = true;
        }

    }

}
