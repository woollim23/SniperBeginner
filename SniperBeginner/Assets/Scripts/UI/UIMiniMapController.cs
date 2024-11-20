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
        //
        foreach(var e in CharacterManager.Instance.enemies)
        {
            AddEnemyIcon(e.transform);
            e.OnEnemyDied += RemoveEnemyIcon;
        }

        mapImageSize = mapImage.sizeDelta;
    }

    private void Update()
    {
        UpdatePlayerIcon();
        UpdateEnemyIcons();
    }

    private void UpdatePlayerIcon()
    {
        Transform playerTransform = CharacterManager.Instance.Player.transform;

        Vector3 playerPosition = playerTransform.position;

        // ���� ��ǥ�� �̴ϸ� ������ ��ȯ (0~1)
        float xRatio = playerPosition.x / mapSize.x;
        float yRatio = playerPosition.z / mapSize.y;

        // �̴ϸ� �߽��� �������� ��ǥ ����
        float xPos = (xRatio - 0.5f) * mapImageSize.x;
        float yPos = (yRatio - 0.5f) * mapImageSize.y;

        // �÷��̾� ������ ��ġ ����
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

            // ���� ��ǥ�� �̴ϸ� ������ ��ȯ (0~1)
            float xRatio = enemyPosition.x / mapSize.x;
            float yRatio = enemyPosition.z / mapSize.y;

            // �̴ϸ� �߽��� �������� ��ǥ ����
            float xPos = (xRatio - 0.5f) * mapImageSize.x;
            float yPos = (yRatio - 0.5f) * mapImageSize.y;

            // �� ������ ��ġ ����
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