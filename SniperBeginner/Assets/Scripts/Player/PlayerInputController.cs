using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInput.PlayerActions Actions => InputManager.Instance.Actions;
}