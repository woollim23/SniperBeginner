using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class Player : MonoBehaviour
{
    // TODO : 스크립터블 오브젝트 전환 고려
    public float WalkSpeed = 3f;


    public PlayerController Controller { get; private set; }
    public PlayerAnimationController AnimationController { get; private set; }
    public CharacterController CharacterController { get; private set;}

    public PlayerStateMachine StateMachine { get; private set; }


    private void Awake() 
    {
        Controller = GetComponent<PlayerController>();
        AnimationController = GetComponent<PlayerAnimationController>();
        CharacterController = GetComponent<CharacterController>();

        StateMachine = new PlayerStateMachine(this);
    }

    private void Update() 
    {
        StateMachine.Update();
    }

}
