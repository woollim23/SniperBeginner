using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiniMapController : MonoBehaviour
{
    [SerializeField] private RectTransform playerIcon;
    [SerializeField] private RectTransform mapImage;
    [SerializeField] private GameObject enemyIconPrefab;
    [SerializeField] private Vector2 mapSize;

    private Vector2 mapImageSize;
    private Dictionary<Transform, RectTransform> enemyIcons = new();

    private void Start()
    {
        //for (int i = 0; i < enemyIcons.Count; i++)
        //{
        //    CharacterManager.Instance.enemies[i].gameObject.GetComponentInChildren<Enemy>().OnEnemySpawned += AddEnemyIcon;
        //}

        //CharacterManager.Instance.enemies.

        //.Instance.OnEnemySpawned += AddEnemyIcon;
        //.Instance.OnEnemyDied += RemoveEnemyIcon;

        mapImageSize = mapImage.sizeDelta;
    }

    private void Update()
    {
        UpdatePlayerIcon();
        UpdateEnemyIcons();
    }

    private void UpdatePlayerIcon()
    {
        Transform playerTransform = CharacterManager.Instance.Player.GetComponent<Transform>();

        Vector3 playerPosition = playerTransform.position;

        // 월드 좌표를 미니맵 비율로 변환 (0~1)
        float xRatio = playerPosition.x / mapSize.x;
        float yRatio = playerPosition.z / mapSize.y;

        // 미니맵 중심을 기준으로 좌표 보정
        float xPos = (xRatio - 0.5f) * mapImageSize.x;
        float yPos = (yRatio - 0.5f) * mapImageSize.y;

        // 플레이어 아이콘 위치 설정
        playerIcon.anchoredPosition = new Vector2(xPos, yPos);
    }

    private void UpdateEnemyIcons()
    {
        foreach (var kvp in enemyIcons)
        {
            Transform enemy = kvp.Key;
            RectTransform icon = kvp.Value;

            if (enemy == null) continue;

            Vector3 enemyPosition = enemy.position;

            // 월드 좌표를 미니맵 비율로 변환 (0~1)
            float xRatio = enemyPosition.x / mapSize.x;
            float yRatio = enemyPosition.z / mapSize.y;

            // 미니맵 중심을 기준으로 좌표 보정
            float xPos = (xRatio - 0.5f) * mapImageSize.x;
            float yPos = (yRatio - 0.5f) * mapImageSize.y;

            // 적 아이콘 위치 설정
            icon.anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    private void AddEnemyIcon(Transform enemy)
    {
        GameObject iconObject = Instantiate(enemyIconPrefab, mapImage);
        RectTransform icon = iconObject.GetComponent<RectTransform>();
        enemyIcons.Add(enemy, icon);
    }

    private void RemoveEnemyIcon(Transform enemy)
    {
        if (!enemyIcons.TryGetValue(enemy, out RectTransform icon)) return;

        StartCoroutine(BlinkAndRemove(icon));
        enemyIcons.Remove(enemy);
    }

    private IEnumerator BlinkAndRemove(RectTransform icon)
    {
        float blinkTime = 1.5f;
        float blinkInterval = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < blinkTime)
        {
            icon.gameObject.SetActive(!icon.gameObject.activeSelf);
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        Destroy(icon.gameObject);
    }
}