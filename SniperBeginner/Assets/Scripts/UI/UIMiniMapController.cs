using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiniMapController : MonoBehaviour
{
    [SerializeField] private RectTransform playerIcon;
    [SerializeField] private RectTransform mapImage;
    [SerializeField] private GameObject enemyIconPrefab;
    [SerializeField] private RectTransform viewCone;
    [SerializeField] private Vector2 mapSize;

    private Vector2 mapImageSize;
    private Dictionary<Transform, RectTransform> enemyIcons = new Dictionary<Transform, RectTransform>();

    private void Start()
    {
        mapImageSize = mapImage.sizeDelta;

        foreach (var enemy in CharacterManager.Instance.enemies)
        {
            AddEnemyIcon(enemy.transform);
            enemy.OnEnemyDied += RemoveEnemyIcon;
        }
    }

    private void Update()
    {
        UpdatePlayerIcon();
        UpdateEnemyIcons();
        UpdateViewCone();
    }

    private void UpdatePlayerIcon()
    {
        Transform playerTransform = CharacterManager.Instance.Player.transform;

        float playerX = playerTransform.position.x;
        float playerZ = playerTransform.position.z;

        float xRatio = (playerX - (-45)) / (53 - (-45));
        float iconX = xRatio * 360 - 180;

        float yRatio = (playerZ - (-48)) / (48 - (-48));
        float iconY = yRatio * 360 - 180;

        playerIcon.anchoredPosition = new Vector2(iconX, iconY);

    }

    private void UpdateEnemyIcons()
    {
        foreach (var kvp in enemyIcons)
        {
            Transform enemy = kvp.Key;
            RectTransform icon = kvp.Value;

            if (enemy == null) continue;

            float enemyX = enemy.position.x;
            float enemyZ = enemy.position.z;

            float xRatio = (enemyX - (-45)) / (53 - (-45));
            float iconX = xRatio * 360 - 180;

            float yRatio = (enemyZ - (-48)) / (48 - (-48));
            float iconY = yRatio * 360 - 180;

            icon.anchoredPosition = new Vector2(iconX, iconY);
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

    private void UpdateViewCone()
    {
        Transform playerTransform = CharacterManager.Instance.Player.transform;

        float playerRotationY = playerTransform.eulerAngles.y;

        viewCone.anchoredPosition = playerIcon.anchoredPosition;

        viewCone.pivot = new Vector2(0.5f, 0.0f);

        viewCone.localRotation = Quaternion.Euler(0, 0, -playerRotationY);
    }
}