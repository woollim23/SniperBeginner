using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, GameObject> uiScreens = new Dictionary<string, GameObject>();

    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private GameObject PauseMenu;

    public GameObject CurrentScreen { get; private set; }

    void Start()
    {
        // UI 화면 등록
        //foreach (Transform child in transform)
        //{
        //    uiScreens[child.name] = child.gameObject;
        //    child.gameObject.SetActive(false);
        //}
        PauseMenuInit();
        InputManager.Instance.OnMenuEvent += OpenPauseMenu;
        //Debug.Log("UIManager");
    }

    public void OpenScreen(string screenName)
    {
        if (uiScreens.ContainsKey(screenName))
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.SetActive(false);
            }

            CurrentScreen = uiScreens[screenName];
            CurrentScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"UI 화면 {screenName}이(가) 등록되지 않았습니다.");
        }
    }


    public void CloseCurrentScreen()
    {
        if (CurrentScreen != null)
        {
            CurrentScreen.SetActive(false);
            CurrentScreen = null;
        }
    }

    public void PauseMenuInit()
    {
        if (MainCanvas != null && PauseMenu != null)
        {
            GameObject pauseMenuInstance = Instantiate(PauseMenu); // 프리펩을 인스턴스화
            pauseMenuInstance.transform.SetParent(MainCanvas.transform, false); // MainCanvas의 자식으로 설정
            PauseMenu = pauseMenuInstance; // 새 인스턴스를 PauseMenu로 등록

            PauseMenu.transform.SetParent(MainCanvas.transform, false);
            //Debug.Log("파우즈메뉴 추가완료");
            PauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("MainCanvas 또는 PauseMenu가 설정되지 않았습니다.");
        }
    }

    public void OpenPauseMenu()
    {
        //Debug.Log("메뉴");
        if (PauseMenu != null)
            PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}
