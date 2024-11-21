using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{

    [SerializeField] private Button loadGameButton; // �ε� ��ư : ����� �����Ͱ� ���� ���� Interactable�� Ȱ��ȭ ��

    private void Start()
    {
        
        DataManager.Instance.LoadGameData(); // ����� ������ �ε�

        // ����� ���� �����Ͱ� �ִ� ��� ��ư ������Ʈ�� Interactable�� Ȱ��ȭ ��
        if (DataManager.Instance.existData)
        {
            loadGameButton.interactable = true;
        }
        else
        {
            loadGameButton.interactable = false;
        }
    }

    public void StartNewGame() // ����� ���� �ҷ����� �ʰ� ���� ����
    {
        // �� ���� ������ �ʱ�ȭ
        DataManager.Instance.CurrentGameData = new GameData
        {
            playerData = new PlayerData(),
            // TODO :: ���ʹ� ������ �ʱ�ȭ
        };

        DataManager.Instance.IsLoadedGame = false; 

        // ���� ���� �� �ε�
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void LoadExistingGame() // ����� ���� �ҷ�����
    {
        DataManager.Instance.IsLoadedGame = true;  // IsLoadedGame�� true�� ���� �� ������ ���� ������ Ȱ����

        // ���� ���� �� �ε�
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
