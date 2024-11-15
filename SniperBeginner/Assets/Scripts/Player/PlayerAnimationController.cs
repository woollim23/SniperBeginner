using UnityEngine;

[System.Serializable]
public class PlayerAnitmationData
{
    public string runParamName = "Run";
    public string horizontalParamName = "Horizontal";
    public string verticalParamName = "Vertical";

    // public string groundParamName = "@Ground";
    public string standParamName = "Stand";
    public string crouchParamName = "Crouch";
    public string crawlParamName = "Crawl";


    public int RunParamHash { get; private set; }
    public int HorizontalParamHash { get; private set; }
    public int VerticalParamHash { get; private set; }
    
    public int StandParamHash { get; private set; }
    public int CrouchParamHash { get; private set; }
    public int CrawlParamHash { get; private set; }

    public void Initialize()
    {
        RunParamHash =          Animator.StringToHash(runParamName);
        HorizontalParamHash =   Animator.StringToHash(horizontalParamName);
        VerticalParamHash =     Animator.StringToHash(verticalParamName);

        StandParamHash =        Animator.StringToHash(standParamName);
        CrouchParamHash =       Animator.StringToHash(crouchParamName);
        CrawlParamHash =        Animator.StringToHash(crawlParamName);
    }
}


[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour 
{
    public PlayerAnitmationData data;

    public Animator Animator { get; private set; }

    private void Awake() 
    {        
        Animator = GetComponent<Animator>();
        
        data = new PlayerAnitmationData();
        data.Initialize();
    }

    public void Move(Vector2 direction)
    {
        Animator.SetFloat(data.HorizontalParamHash, direction.x);
        Animator.SetFloat(data.VerticalParamHash, direction.y);
    }
}
