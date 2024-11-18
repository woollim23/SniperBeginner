using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInput.PlayerActions Actions { get; private set; }

    private void Awake() 
    {
        Actions = InputManager.Instance.Actions;
    }
}