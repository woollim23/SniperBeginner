using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Player UI")]
    [SerializeField] CanvasGroup prefabPlayerCanvas;
    [SerializeField] UIAmmoInfo prefabAmmoInfo;
    [SerializeField] UIQuickSlotManager prefabQuickSlot;
    [SerializeField] UIMiniMapController prefabMiniMap;
    [SerializeField] TextMeshProUGUI prefabScore;
    [SerializeField] GameObject prefabPauseMenu;
    [SerializeField] GameObject prefabGameOverMenu;
    [SerializeField] GameObject prefabGameClearMenu;

    public CanvasGroup PlayerCanvas { get; private set; }
    public UIAmmoInfo AmmoInfo { get; private set; }
    public UIQuickSlotManager QuickSlot { get; private set; }
    public UIMiniMapController MiniMap { get; private set; }
    public TextMeshProUGUI Score { get; private set; }
    public GameObject PauseMenu { get; private set; }


    void Start()
    {
        GameManager.Instance.onChangeScore += OnChangeScore;
        OnChangeScore();

        InputManager.Instance.OnMenuEvent += OpenPauseMenu;
        SetCursor(true);
    }

    public void Initialize()
    {
        PlayerCanvas = Instantiate(prefabPlayerCanvas);

        AmmoInfo = Instantiate(prefabAmmoInfo, PlayerCanvas.transform);
        QuickSlot = Instantiate(prefabQuickSlot, PlayerCanvas.transform);
        MiniMap = Instantiate(prefabMiniMap, PlayerCanvas.transform);
        Score = Instantiate(prefabScore, PlayerCanvas.transform);
        PauseMenu = Instantiate(prefabPauseMenu, PlayerCanvas.transform);
    }

    private void OnChangeScore()
    {
        if(Score != null)
            Score.text = GameManager.Instance.Score + " / " + CharacterManager.Instance.enemies.Count;
    }

    public void OpenPauseMenu()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        InputManager.Instance.Actions.Disable();
        SetCursor(false);
    }

    public void SetCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void OpenGameOverMenu()
    {
        Instantiate(prefabGameOverMenu, PlayerCanvas.transform);

        Time.timeScale = 0f;
        InputManager.Instance.Actions.Disable();
        SetCursor(false);
    }

    public void OpenGameClearMenu()
    {
        Instantiate(prefabGameClearMenu, PlayerCanvas.transform);

        Time.timeScale = 0f;
        InputManager.Instance.Actions.Disable();
        SetCursor(false);
    }
}
