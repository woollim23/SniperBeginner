using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, GameObject> uiScreens = new Dictionary<string, GameObject>();

    private GameObject currentScreen;

    void Start()
    {
        // UI 화면 등록
        foreach (Transform child in transform)
        {
            uiScreens[child.name] = child.gameObject;
            child.gameObject.SetActive(false);
        }
    }

    public void OpenScreen(string screenName)
    {
        if (uiScreens.ContainsKey(screenName))
        {
            if (currentScreen != null)
            {
                currentScreen.SetActive(false);
            }

            currentScreen = uiScreens[screenName];
            currentScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"UI 화면 {screenName}이(가) 등록되지 않았습니다.");
        }
    }

    /// <summary>
    /// UI 화면 닫기
    /// </summary>
    public void CloseCurrentScreen()
    {
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
            currentScreen = null;
        }
    }
}
