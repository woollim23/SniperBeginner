using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenPanel : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadSceneAsync("MainGame");

        GameManager.Instance.GameStartInit();
    }

    public void GameLoad()
    {
        Debug.Log("���� ���� �ҷ�����");
        SceneManager.LoadSceneAsync("MainGame");
        // ���� �ҷ����� ��ư
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
