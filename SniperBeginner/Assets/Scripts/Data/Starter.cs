using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{

    [SerializeField] private Button loadGameButton; // 로드 버튼 : 저장된 데이터가 있을 때만 Interactable이 활성화 됨

    private void Start()
    {
        DataManager.Instance.LoadGameData(); // 저장된 데이터 로드

        // 저장된 게임 데이터가 있는 경우 버튼 컴포넌트의 Interactable이 활성화 됨
        if (DataManager.Instance.existData)
        {
            loadGameButton.interactable = true;
        }
        else
        {
            loadGameButton.interactable = false;
        }

        SoundManager.Instance.PlayBackgroundMusic(SoundManager.Instance.titleBGM);
    }

    public void StartNewGame() // 저장된 게임 불러오지 않고 게임 시작
    {
        // 새 게임 데이터 초기화
        DataManager.Instance.CurrentGameData = new GameData
        {
            playerData = new PlayerData(),
            // TODO :: 에너미 데이터 초기화
        };

        DataManager.Instance.IsLoadedGame = false; 

        // 메인 게임 씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void LoadExistingGame() // 저장된 게임 불러오기
    {
        DataManager.Instance.IsLoadedGame = true;  // IsLoadedGame을 true로 설정 → 저장한 게임 데이터 활용함

        // 메인 게임 씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
