using UnityEngine;

public class PlayerAnimationController : MonoBehaviour 
{
    Animator animator;

    private void Awake() 
    {        
        animator = GetComponent<Animator>();
    }    
}
