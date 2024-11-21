using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : SingletonDontDestory<GameManager>
{

    public GameData GameData { get; private set; }
    public int Score { get; set; } = 0;
    public bool isGameOver { get; private set; }

    public event Action onChangeScore;
    
    public override void Awake() 
    {
        base.Awake();

        // 게임 내에서만 쓰는 매니저들 Initialize
        CharacterManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        CameraManager.Instance.Initialize();
    }

    private void Start()
    {
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
        if (GameData == null)
        {
            GameData = new GameData
            {
                playerData = new PlayerData()
            };
        }

        // 플레이어 데이터 저장
        Player player = CharacterManager.Instance.Player; // Player 인스턴스 가져오기
        if (player != null)
        {
            GameData.playerData.Position = player.transform.position; // 플레이어 위치 저장
            GameData.playerData.Health = player.Condition.Health; // 플레이어 체력 저장
            GameData.playerData.EquippedWeaponIndex = player.Equipment.allWeapons.IndexOf(player.Equipment.CurrentEquip.weaponData); // 현재 총기 저장
            // 현재 총알 수 저장
            GameData.playerData.CurrentAmmoInMagazine = new List<int>();
            foreach (var weapon in player.Equipment.weaponInstance)
            {
                GameData.playerData.CurrentAmmoInMagazine.Add(weapon.currentAmmoInMagazine);
            }
        }

        // 적 데이터 저장
        GameData.enemyData.Clear(); // 기존 적 데이터 초기화
        foreach (var enemy in CharacterManager.Instance.enemies)
        {
            if (enemy.Health > 0) // 살아 있는 적만 저장
            {
                EnemyData enemyData = new EnemyData
                {
                    Position = enemy.transform.position,
                    Health = enemy.Health
                };
                GameData.enemyData.Add(enemyData);
            }
        }

        DataManager.Instance.SaveGameData(GameData);
    }


    public void LoadGame()
    {
        DataManager.Instance.LoadGameData();
        GameData = DataManager.Instance.CurrentGameData;

        // 총알 불러오기
        // 동적 생성한 플레이어 받기
        //for (int i = 0; i < GameData.playerData.CurrentAmmoInMagazine.Count; i++)
        //{
        //    if (i < player.Equipment.weaponInstance.Count)
        //    {
        //        player.Equipment.weaponInstance[i].currentAmmoInMagazine = GameData.playerData.CurrentAmmoInMagazine[i];
        //    }
        //}

        // 적 데이터 복원 - enemyPrefab 보호수준 확인하기
        //CharacterManager.Instance.enemies.Clear(); // 기존 적 삭제
        //foreach (var enemyData in GameData.enemyData)
        //{
        //    Enemy newEnemy = Instantiate(CharacterManager.Instance.enemyPrefab, enemyData.Position, Quaternion.identity); 
        //    newEnemy.Health = enemyData.Health; // 체력 설정
        //    CharacterManager.Instance.enemies.Add(newEnemy);
        //}

    }

}
