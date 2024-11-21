using System.Collections.Generic;

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
}
