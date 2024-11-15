using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour 
{
    public readonly int Run = Animator.StringToHash("Run");
    public readonly int Vertical = Animator.StringToHash("Vertical");
    public readonly int Horizontal = Animator.StringToHash("Horizontal");

    Animator animator;

    private void Awake() 
    {        
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 direction)
    {
        animator.SetFloat(Horizontal, direction.x);
        animator.SetFloat(Vertical, direction.y);
    }

}
