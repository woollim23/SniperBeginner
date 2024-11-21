using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(PlayerShootingController))]
[RequireComponent(typeof(PlayerView))]
[RequireComponent(typeof(PlayerEquipment))]
[RequireComponent(typeof(PlayerCondition))]
[RequireComponent(typeof(PlayerInteraction))]
[RequireComponent(typeof(ForceReceiver))]
public class Player : MonoBehaviour
{
    public PlayerSetting setting;
    public PlayerInput.PlayerActions Actions => InputManager.Instance.Actions;

    public CharacterController Controller { get; private set;}

    public PlayerAnimationController Animation { get; private set; }
    public PlayerShootingController Shooting { get; private set; }
    public PlayerView View { get; private set; }
    public PlayerEquipment Equipment { get; private set; }
    public PlayerCondition Condition { get; private set; }
    public PlayerInteraction Interact { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    
    public PlayerStateMachine StateMachine { get; private set; }


    private void Awake() 
    {
        CharacterManager.Instance.Player = this;

        Controller = GetComponent<CharacterController>();
        Animation = GetComponent<PlayerAnimationController>();
        Shooting = GetComponent<PlayerShootingController>();
        View = GetComponent<PlayerView>();
        Equipment = GetComponent<PlayerEquipment>();
        Condition = GetComponent<PlayerCondition>();
        Interact = GetComponent<PlayerInteraction>();
        ForceReceiver = GetComponent<ForceReceiver>();

        StateMachine = new PlayerStateMachine(this);
    }


    private void Update() 
    {
        StateMachine.Update();
    }

    private void FixedUpdate() 
    {
        StateMachine.FixedUpdate();
    }

    public void Initialize(PlayerData data)
    {
        if (data != null)
        {
            // 위치 및 체력 복구
            transform.position = data.Position;
            Condition.Health = data.Health;           
        }

        // 시작은 일반 - 서 있는 상태
        StateMachine.ChangeState(StateMachine.StandState);
    }
}

[System.Serializable]
public class PlayerSetting
{
    // 필요하면 scriptable object로 분리
    [Header("Move")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float crouchSpeed = 2f;
    [Range(0.01f, 1f)] public float movementInputSmoothness = 0.05f;
    
    [Header("Jump")]
    public float jumpPower = 20f;

    [Header("Fall")]
    public float fallThreshold = 1f;

    [Header("Look")]
    public float lookSensitive = 1f;
    public Vector2 lookYAxisLimit = new Vector2(-80f, 80f);

    [Header("Breathing")]
    public float breathAmountOnRelax = 1f;
    public float decayBreathWhileControl = 1f;
    
    public float minBreathForControl = 3f; // 숨을 제어하려면 최소 3은 있어야 함
    public float maxBreath = 10f;
}
