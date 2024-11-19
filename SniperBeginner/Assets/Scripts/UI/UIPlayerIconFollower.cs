using UnityEngine;

public class UIPlayerIconFollower : MonoBehaviour
{
    public Transform player;
    public RectTransform miniMapIcon;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void Update()
    {
        Vector3 playerPosition = player.position + offset;
        miniMapIcon.position = new Vector3(playerPosition.x, playerPosition.z, 0);
    }
}