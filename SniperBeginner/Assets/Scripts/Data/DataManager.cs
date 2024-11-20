using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager 
{
    private static string gameDataPath => Application.dataPath + "/GameData.json"; // TODO:: ���� ������ Application.persistentDataPath�� �ٲٱ�

    public static void SaveGameData(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(gameDataPath, json);
    }

    public static GameData LoadGameData()
    {
        if (!File.Exists(gameDataPath)) // ���� ���� Ȯ��
        {
            Debug.Log("���� ���� ����");
            return new GameData // �⺻ ���� ��ȯ
            {
                playerData = new PlayerData(),
                // ���ʹ� ������
                // �κ��丮 ������
            };
        }

        string json = File.ReadAllText(gameDataPath);
        return JsonUtility.FromJson<GameData>(json);
    }

}
