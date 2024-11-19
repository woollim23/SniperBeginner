using UnityEngine;

public class UIEnemyIconFollower : MonoBehaviour
{
    public RectTransform enemyIcon;  // 적 아이콘 RectTransform
    public Transform enemy;  // 적 Transform
    public Transform player;  // 플레이어 Transform
    public Transform miniMapCamera;  // 미니맵 카메라 Transform
    public float miniMapScale = 0.1f;  // 미니맵 스케일 비율

    private void Update()
    {
        // 적과 플레이어 사이의 상대 위치 계산
        Vector3 relativePosition = enemy.position - player.position;

        // 미니맵 스케일 적용
        Vector2 miniMapPosition = new Vector2(relativePosition.x, relativePosition.z) * miniMapScale;

        // 미니맵 카메라의 회전을 반영하여 위치 보정
        float angle = -miniMapCamera.eulerAngles.y; // 미니맵 카메라의 Y축 회전값
        miniMapPosition = RotatePoint(miniMapPosition, angle);

        // 적 아이콘 위치 업데이트
        enemyIcon.localPosition = miniMapPosition;

        // 적 아이콘의 회전 (선택 사항)
        enemyIcon.localEulerAngles = new Vector3(0, 0, -enemy.eulerAngles.y + miniMapCamera.eulerAngles.y);
    }

    // 회전 보정을 위한 2D 점 회전 함수
    private Vector2 RotatePoint(Vector2 point, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        float x = point.x * cos - point.y * sin;
        float y = point.x * sin + point.y * cos;

        return new Vector2(x, y);
    }
}