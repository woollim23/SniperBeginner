
public class InputManager : Singleton<InputManager>
{
    // 플레이어 입력에 대한 접근성을 위해서 별도로 매니저를 만듦
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