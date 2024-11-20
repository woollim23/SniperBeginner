using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Player UI")]
    [SerializeField] CanvasGroup prefabPayerCanvas;
    [SerializeField] UIAmmoInfo prefabAmmoInfo;
    [SerializeField] UIQuickSlotManager prefabQuickSlot;
    [SerializeField] UIMiniMapController prefabMiniMap;
    [SerializeField] TextMeshProUGUI prefabScore;
    [SerializeField] GameObject prefabPauseMenu;

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
        PlayerCanvas = Instantiate(prefabPayerCanvas);

        AmmoInfo = Instantiate(prefabAmmoInfo, playerCanvas.transform);
        QuickSlot = Instantiate(prefabQuickSlot, playerCanvas.transform);
        MiniMap = Instantiate(prefabMiniMap, playerCanvas.transform);
        Score = Instantiate(prefabScore, playerCanvas.transform);
        PauseMenu = Instantiate(prefabPauseMenu, playerCanvas.transform);
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
}
