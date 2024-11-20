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
        SceneManager.LoadSceneAsync("MainGame");
        GameManager.Instance.LoadGame();
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
