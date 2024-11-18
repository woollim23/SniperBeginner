using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void GameResume()
    {
        //if (pausePanel != null)
        //    pausePanel.SetActive(false); // �޴� UI ��Ȱ��ȭ
        Time.timeScale = 1f;          // ���� �簳
    }

    public void GameSave()
    {
        // ���� ���� ��ư
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
