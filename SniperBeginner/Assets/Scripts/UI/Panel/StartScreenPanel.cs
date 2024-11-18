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
        Debug.Log("저장 게임 불러오기");
        SceneManager.LoadSceneAsync("MainGame");
        // 저장 불러오기 버튼
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
