using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestory<GameManager>
{
    private GameObject pausePanel;

    public int deadEnemyCnt;
    public bool isGameOver;
    public bool isPaused;

    public void GameStartInit()
    {
        // 게임 시작 초기화 함수
        deadEnemyCnt = 0;
        isGameOver = false;
        isPaused = false;

        Invoke("UIInit", 0.5f);
    }

    public void UIInit()
    {
        GameObject canvas = GameObject.Find("MainUI");

        pausePanel = canvas.transform.Find("PausePanel")?.gameObject;
    }

    public void GameClear()
    {
        // 게임 클리어 확인 함수
    }

    public void CountDeadEnemy()
    {
        // 적 처치 카운팅 함수
    }

    public void OnPauseMenu()
    {
        pausePanel.SetActive(true); // 메뉴 UI 활성화
        Time.timeScale = 0f;        // 게임 멈춤
        isPaused = true;            // 멈춤 상태 업데이트
    }

    public void ButtonGameStart()
    {
        SceneManager.LoadSceneAsync("MainGame");

        GameStartInit();
    }

    public void ButtonGameResume()
    {
        pausePanel.SetActive(false); // 메뉴 UI 비활성화
        Time.timeScale = 1f;          // 게임 재개
        isPaused = false;             // 멈춤 상태 업데이트
    }

    public void ButtonGameSave()
    {
        // 게임 저장 버튼
    }

    public void ButtonGameLoad()
    {
        // 저장 불러오기 버튼
    }

    public void ButtonGameTitle()
    {
        SceneManager.LoadSceneAsync("Title");
    }

    public void ButtonGameExit()
    {
        // 게임 종료 함수
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
