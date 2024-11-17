using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInput Input { get; private set; }
    public PlayerInput.PlayerActions Actions { get; private set; }

    private void Awake() 
    {
        Input = new PlayerInput();
        Actions = Input.Player;
    }

    private void OnEnable() 
    {
        Input.Enable();
    }

    private void OnDisable() 
    {
        Input.Disable();    
    }
}