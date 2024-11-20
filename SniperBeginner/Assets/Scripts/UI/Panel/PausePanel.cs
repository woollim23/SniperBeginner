using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void GameResume()
    {
        Debug.Log("Resume");
        gameObject.SetActive(false); // �޴� UI ��Ȱ��ȭ
        Time.timeScale = 1f;          // ���� �簳
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
