using System;

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
        Player player = CharacterManager.Instance.Player; // Player 인스턴스 가져오기
        if (player != null)
        {
            GameData.playerData.Position = player.transform.position; // 플레이어 위치 저장
            GameData.playerData.Health = player.Condition.Health; // 플레이어 체력 저장
            // 플레이어 장비 저장
            // 플레이어 총알 저장
        }

        DataManager.SaveGameData(GameData);
    }

}
