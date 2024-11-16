using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(PlayerView))]
[RequireComponent(typeof(PlayerEquipment))]
public class Player : MonoBehaviour
{
    public PlayerSetting setting;

    public CharacterController Controller { get; private set;}
    public PlayerInputController Input { get; private set; }
    public PlayerAnimationController Animation { get; private set; }
    public PlayerView View { get; private set; }
    public PlayerEquipment Equipment { get; private set; }
    
    public PlayerStateMachine StateMachine { get; private set; }


    private void Awake() 
    {
        CharacterManager.Instance.Player = this;

        Controller = GetComponent<CharacterController>();
        Input = GetComponent<PlayerInputController>();
        Animation = GetComponent<PlayerAnimationController>();
        View = GetComponent<PlayerView>();
        Equipment = GetComponent<PlayerEquipment>();

        StateMachine = new PlayerStateMachine(this);
    }

    private void Start() 
    {
        // 시작은 일반 - 서 있는 상태
        StateMachine.ChangeState(StateMachine.StandState);    
    }

    private void Update() 
    {
        StateMachine.Update();
    }

}

[System.Serializable]
public class PlayerSetting
{
    // 필요하면 scriptable object로 분리
    [Header("Move")]
    public float WalkSpeed = 3f;
    public float RunSpeed = 6f;
    [Range(0.01f, 1f)] public float MovementInputSmoothness = 0.05f;

    [Header("Look")]
    public float lookSensitive = 1f;
    public Vector2 lookYAxisLimit = new Vector2(-80f, 80f);
}
