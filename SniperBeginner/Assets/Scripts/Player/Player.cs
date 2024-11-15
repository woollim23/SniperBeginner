using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class Player : MonoBehaviour
{
    // TODO : 스크립터블 오브젝트 전환
    public float WalkSpeed = 3f;
    public float RunSpeed = 3f;
    // public float Movement;


    public CharacterController Controller { get; private set;}
    public PlayerInputController Input { get; private set; }
    public PlayerAnimationController Animation { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }


    private void Awake() 
    {
        Controller = GetComponent<CharacterController>();
        Input = GetComponent<PlayerInputController>();
        Animation = GetComponent<PlayerAnimationController>();

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
