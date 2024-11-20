using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenPanel : MonoBehaviour
{
    // 새 게임 시작
    public void GameStart()
    {
        SceneManager.LoadSceneAsync("MainGame");
    }

    // 로드한 게임 시작
    public void StartLoadedGame()
    {
        SceneManager.LoadSceneAsync("MainGame");
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
