using UnityEngine;

public class UIMiniMapCameraFollower : MonoBehaviour
{
    public Transform player; // 플레이어 Transform

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}