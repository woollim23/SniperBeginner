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
        if (!File.Exists(PlayerDataPath)) // ���� ���� Ȯ��
        {
            Debug.Log("���� ���� ����");
            return new PlayerData(); // �⺻ ���� ��ȯ
        }

        string json = File.ReadAllText(PlayerDataPath);
        return JsonUtility.FromJson<PlayerData>(json);
    }

}
