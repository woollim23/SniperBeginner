using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void GameResume()
    {
        //if (pausePanel != null)
        //    pausePanel.SetActive(false); // 메뉴 UI 비활성화
        Time.timeScale = 1f;          // 게임 재개
    }

    public void GameSave()
    {
        // 게임 저장 버튼
        Debug.Log("save");
    }

    public void GameTitle()
    {
        SceneManager.LoadSceneAsync("Title");
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
