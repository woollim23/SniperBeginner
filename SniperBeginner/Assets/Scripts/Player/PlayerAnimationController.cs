using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour 
{
    public string runParamName = "Run";
    public string horizontalParamName = "Horizontal";
    public string verticalParamName = "Vertical";

    // public string groundParamName = "@Ground";
    public string standParamName = "Stand";
    public string crouchParamName = "Crouch";
    public string crawlParamName = "Crawl";

    public string fireParamName = "Fire";
    public string aimingParamName = "Aiming";


    public int RunParamHash { get; private set; }
    public int HorizontalParamHash { get; private set; }
    public int VerticalParamHash { get; private set; }
    
    public int StandParamHash { get; private set; }
    public int CrouchParamHash { get; private set; }
    public int CrawlParamHash { get; private set; }

    public int FireParamHash { get; private set; }
    public int AimingParamHash { get; private set; }
    
    public Animator Animator { get; private set; }


    private void Awake() 
    {        
        Animator = GetComponent<Animator>();
        
        Initialize();
    }

    
    public void Initialize()
    {
        RunParamHash =          Animator.StringToHash(runParamName);
        HorizontalParamHash =   Animator.StringToHash(horizontalParamName);
        VerticalParamHash =     Animator.StringToHash(verticalParamName);

        StandParamHash =        Animator.StringToHash(standParamName);
        CrouchParamHash =       Animator.StringToHash(crouchParamName);
        CrawlParamHash =        Animator.StringToHash(crawlParamName);

        FireParamHash =         Animator.StringToHash(fireParamName);
        AimingParamHash =       Animator.StringToHash(aimingParamName);
    }

    public void Move(Vector2 direction)
    {
        Animator.SetFloat(HorizontalParamHash, direction.x);
        Animator.SetFloat(VerticalParamHash, direction.y);
    }

    public void Fire()
    {
        Animator.SetTrigger(FireParamHash);
    }

    public void Aiming(bool isOn)
    {
        Animator.SetBool(AimingParamHash, isOn);
    }
}
