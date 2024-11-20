using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager // 싱글톤 - 돈디스트로이
{
    public bool IsLoadedGame = false;
    
    private static string gameDataPath => Application.dataPath + "/GameData.json"; 
    // TODO:: 문제 없으면 Application.persistentDataPath로 바꾸기

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
            return null;
        }

        string json = File.ReadAllText(gameDataPath);
        return JsonUtility.FromJson<GameData>(json);
    }

}
