﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestory<GameManager>
{
    private GameObject pausePanel;

    public int deadEnemyCnt;
    public bool isGameOver;

    public void GameStartInit()
    {
        // 게임 시작 초기화 함수
        deadEnemyCnt = 0;
        isGameOver = false;
    }

    public void GameClear()
    {
        // 게임 클리어 확인 함수
    }

    public void CountDeadEnemy()
    {
        // 적 처치 카운팅 함수
    }
    //---------UI 매니저------
    public void UIInit()
    {
        GameObject canvas = GameObject.Find("MainUI");

        pausePanel = canvas.transform.Find("PausePanel")?.gameObject;
    }

    public void OnPauseMenu()
    {
        pausePanel.SetActive(true); // 메뉴 UI 활성화
        Time.timeScale = 0f;        // 게임 멈춤
    }
}
