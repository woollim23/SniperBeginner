﻿using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private TextMeshProUGUI Score;

    public GameObject CurrentScreen { get; private set; }

    private void Awake()
    {
        Score = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {       
        GameManager.Instance.onChangeScore += OnChangeScore;
        OnChangeScore();

        PauseMenuInit();
        InputManager.Instance.OnMenuEvent += OpenPauseMenu;
        SetCursor(true);
    }

    private void OnChangeScore()
    {
        Debug.Log(GameManager.Instance.enemies.Count);
        Score.text = GameManager.Instance.enemies.Count + " / " + GameManager.Instance.enemies.Capacity;
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
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        InputManager.Instance.Actions.Disable();
        SetCursor(false);
    }

    private bool Toggle(GameObject gameObject)
    {
        if (gameObject.activeSelf)
            return true;
        else
            return false;
    }

    public void SetCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
