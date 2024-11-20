using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void GameResume()
    {
        Debug.Log("Resume");
        gameObject.SetActive(false); // 메뉴 UI 비활성화
        Time.timeScale = 1f;          // 게임 재개
        InputManager.Instance.Actions.Enable();
        UIManager.Instance.SetCursor(true);
    }

    public void GameSave()
    {
        GameManager.Instance.SaveGame();
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
