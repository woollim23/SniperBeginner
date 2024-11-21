using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager
{
    public void SaveGame(GameManager gameManager)
    {
        if (gameManager.GameData == null)
        {
            gameManager.GameData = new GameData
            {
                playerData = new PlayerData()
            };
        }

        SavePlayerData(gameManager);
        SaveEnemyData(gameManager);

        DataManager.Instance.SaveGameData(gameManager.GameData);
    }

    public void LoadGame(GameManager gameManager)
    {
        DataManager.Instance.LoadGameData();
        gameManager.GameData = DataManager.Instance.CurrentGameData;

        // LoadPlayerData(gameManager);
        // LoadEnemyData(gameManager);
    }

    private void SavePlayerData(GameManager gameManager)
    {
        Player player = CharacterManager.Instance.Player;
        if (player != null)
        {
            gameManager.GameData.playerData.Position = player.transform.position;
            gameManager.GameData.playerData.Health = player.Condition.Health;
            gameManager.GameData.playerData.EquippedWeaponIndex = player.Equipment.allWeapons.IndexOf(player.Equipment.CurrentEquip.weaponData);

            gameManager.GameData.playerData.CurrentAmmoInMagazine = new List<int>();
            foreach (var weapon in player.Equipment.weaponInstance)
            {
                gameManager.GameData.playerData.CurrentAmmoInMagazine.Add(weapon.currentAmmoInMagazine);
            }
        }
    }

    private void SaveEnemyData(GameManager gameManager)
    {
        gameManager.GameData.enemyData.Clear();
        foreach (var enemy in CharacterManager.Instance.enemies)
        {
            if (enemy.Health > 0)
            {
                EnemyData enemyData = new EnemyData
                {
                    Position = enemy.transform.position,
                    Health = enemy.Health
                };
                gameManager.GameData.enemyData.Add(enemyData);

                Debug.Log($"�� ����: ��ġ {enemy.transform.position}, ü�� {enemy.Health}");
            }
        }
    }

    //private void LoadPlayerData(GameManager gameManager)
    //{
    //    Player player = CharacterManager.Instance.Player;
    //    if (player != null && gameManager.GameData.playerData != null)
    //    {
    //        player.transform.position = gameManager.GameData.playerData.Position;
    //        player.Condition.Health = gameManager.GameData.playerData.Health;

    //        for (int i = 0; i < gameManager.GameData.playerData.CurrentAmmoInMagazine.Count; i++)
    //        {
    //            if (i < player.Equipment.weaponInstance.Count)
    //            {
    //                player.Equipment.weaponInstance[i].currentAmmoInMagazine = gameManager.GameData.playerData.CurrentAmmoInMagazine[i];
    //            }
    //        }
    //    }
    //}

    //private void LoadEnemyData(GameManager gameManager)
    //{
    //    CharacterManager.Instance.enemies.Clear();
    //    foreach (var enemyData in gameManager.GameData.enemyData)
    //    {
    //        Enemy newEnemy = Object.Instantiate(CharacterManager.Instance.enemyPrefab, enemyData.Position, Quaternion.identity);
    //        newEnemy.Health = enemyData.Health;
    //        CharacterManager.Instance.enemies.Add(newEnemy);
    //    }
    //}
}
