using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager
{
    public void SaveGame()
    {
        if (DataManager.Instance.CurrentGameData == null)
        {
            DataManager.Instance.CurrentGameData = new GameData
            {
                playerData = new PlayerData()
            };
        }

        SavePlayerData();
        SaveEnemyData();

        DataManager.Instance.SaveGameData();
    }

    public void LoadGame()
    {
        DataManager.Instance.LoadGameData();
        // LoadPlayerData(gameManager);
        // LoadEnemyData(gameManager);
    }

    private void SavePlayerData()
    {
        Player player = CharacterManager.Instance.Player;
        GameData data = DataManager.Instance.CurrentGameData;
        if (player != null)
        {
            data.playerData.Position = player.transform.position;
            data.playerData.Health = player.Condition.Health;
            data.playerData.EquippedWeaponIndex = player.Equipment.allWeapons.IndexOf(player.Equipment.CurrentEquip.weaponData) + 1;

            data.playerData.CurrentAmmoInMagazine = new List<int>();
            foreach (var weapon in player.Equipment.weaponInstance)
            {
                data.playerData.CurrentAmmoInMagazine.Add(weapon.currentAmmoInMagazine);
            }
        }
    }

    private void SaveEnemyData()
    {
        GameData data = DataManager.Instance.CurrentGameData;
        data.enemyData.Clear();

        foreach (var enemy in CharacterManager.Instance.enemies)
        {
            if (enemy.Health > 0)
            {
                EnemyData enemyData = new EnemyData
                {
                    Position = enemy.transform.position,
                    Health = enemy.Health
                };
                data.enemyData.Add(enemyData);
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
