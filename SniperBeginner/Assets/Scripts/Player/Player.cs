using UnityEngine;


public class Player : MonoBehaviour
{
    public PlayerController Controller {get; private set;}

    // 상태
    // 애니메이션

    private void Awake() 
    {
        Controller = GetComponent<PlayerController>();
    }

}
