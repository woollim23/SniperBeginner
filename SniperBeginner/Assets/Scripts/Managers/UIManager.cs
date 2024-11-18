using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, GameObject> uiScreens = new Dictionary<string, GameObject>();

    private GameObject currentScreen;

    void Start()
    {
        // UI ȭ�� ���
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
            Debug.LogWarning($"UI ȭ�� {screenName}��(��) ��ϵ��� �ʾҽ��ϴ�.");
        }
    }

    /// <summary>
    /// UI ȭ�� �ݱ�
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
