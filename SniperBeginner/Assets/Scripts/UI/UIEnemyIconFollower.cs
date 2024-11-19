using UnityEngine;

public class UIEnemyIconFollower : MonoBehaviour
{
    public RectTransform enemyIcon;  // �� ������ RectTransform
    public Transform enemy;  // �� Transform
    public Transform player;  // �÷��̾� Transform
    public Transform miniMapCamera;  // �̴ϸ� ī�޶� Transform
    public float miniMapScale = 0.1f;  // �̴ϸ� ������ ����

    private void Update()
    {
        // ���� �÷��̾� ������ ��� ��ġ ���
        Vector3 relativePosition = enemy.position - player.position;

        // �̴ϸ� ������ ����
        Vector2 miniMapPosition = new Vector2(relativePosition.x, relativePosition.z) * miniMapScale;

        // �̴ϸ� ī�޶��� ȸ���� �ݿ��Ͽ� ��ġ ����
        float angle = -miniMapCamera.eulerAngles.y; // �̴ϸ� ī�޶��� Y�� ȸ����
        miniMapPosition = RotatePoint(miniMapPosition, angle);

        // �� ������ ��ġ ������Ʈ
        enemyIcon.localPosition = miniMapPosition;

        // �� �������� ȸ�� (���� ����)
        enemyIcon.localEulerAngles = new Vector3(0, 0, -enemy.eulerAngles.y + miniMapCamera.eulerAngles.y);
    }

    // ȸ�� ������ ���� 2D �� ȸ�� �Լ�
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