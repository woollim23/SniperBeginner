using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager 
{
    private static string PlayerDataPath => Application.dataPath + "/PlayerData.json";

    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PlayerDataPath, json);
    }

    public static PlayerData LoadPlayerData()
    {
        if (!File.Exists(PlayerDataPath)) // 저장 파일 확인
        {
            Debug.Log("저장 파일 없음");
            return new PlayerData(); // 기본 세팅 반환
        }

        string json = File.ReadAllText(PlayerDataPath);
        return JsonUtility.FromJson<PlayerData>(json);
    }

}
