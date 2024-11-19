using UnityEngine;

public class UIPlayerIconFollower : MonoBehaviour
{
    public Transform player;
    public RectTransform miniMapIcon;

    private Vector3 offset;

    private void Start()
    {
        offset = miniMapIcon.position - player.position;
    }

    private void Update()
    {
        if (player == null || miniMapIcon == null) return;

        Vector3 playerPosition = player.position;
        miniMapIcon.position = new Vector3(playerPosition.x + offset.x, playerPosition.z + offset.y, 0);
    }
}