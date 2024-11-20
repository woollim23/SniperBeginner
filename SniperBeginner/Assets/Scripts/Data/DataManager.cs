using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager 
{
    private static string gameDataPath => Application.dataPath + "/GameData.json"; // TODO:: 문제 없으면 Application.persistentDataPath로 바꾸기

    public static void SaveGameData(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(gameDataPath, json);
    }

    public static GameData LoadGameData()
    {
        if (!File.Exists(gameDataPath)) // 저장 파일 확인
        {
            Debug.Log("저장 파일 없음");
            return new GameData // 기본 세팅 반환
            {
                playerData = new PlayerData(),
                // 에너미 데이터
                // 인벤토리 데이터
            };
        }

        string json = File.ReadAllText(gameDataPath);
        return JsonUtility.FromJson<GameData>(json);
    }

}
