using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimationController : MonoBehaviour 
{
    public AnimationData data;
        
    [field:Space(10f)]
    [field:SerializeField] public Animator Animator { get; private set; }
    
    [Header("IK")]
    [SerializeField] RigBuilder rigBuilder;
    [SerializeField] Rig rig;
    [SerializeField] MultiAimConstraint aimConstraint;
    [SerializeField] ChainIKConstraint leftHandIK;
    

    public Transform aimIKTarget; // IK point로 쓸 것
    [SerializeField] Transform leftHandIKTarget;
    [SerializeField] Vector3 leftHandOffset;

    private void Awake() 
    {        
        if(!Animator)
            Animator = GetComponentInChildren<Animator>();
        
        data = new AnimationData();
        data.Initialize();

        if(!aimIKTarget)
        {
            aimIKTarget = new GameObject("Aim IK Target").transform;
            
            var data = aimConstraint.data.sourceObjects;
            data.SetTransform(0, aimIKTarget);
            aimConstraint.data.sourceObjects = data;

            rigBuilder.Build();
        }
        
        aimIKTarget.SetParent(null);
    }

    public void ModifyLeftHandIK(Vector3 targetPosition)
    {
        leftHandIKTarget.position = targetPosition;
        leftHandIKTarget.localPosition += leftHandOffset;
    }


    public void Move(Vector2 direction)
    {
        Animator.SetFloat(data.HorizontalParamHash, direction.x);
        Animator.SetFloat(data.VerticalParamHash, direction.y);
    }

    public void Aiming(bool isOn)
    {
        Animator.SetBool(data.AimingParamHash, isOn);
    }

    public void AimingModifier(float value)
    {
        Animator.SetFloat(data.AimingModifierParamHash, value);
    }

    public void Fire()
    {
        Animator.SetTrigger(data.FireParamHash);
    }

    public void Jump()
    {
        Animator.SetTrigger(data.JumpParamHash);
    }

    public void InGround(bool isOn)
    {
        Animator.SetBool(data.GroundParamHash, isOn);
    }
    
    public void InAir(bool isOn)
    {
        Animator.SetBool(data.AirParamHash, isOn);
    }

    public void Fall()
    {
        Animator.SetTrigger(data.FallParamHash);
    }

    public void Reload()
    {
        Animator.SetTrigger(data.ReloadParamHash);
    }
}

[System.Serializable]
public class AnimationData
{
    public string runParamName = "Run";
    public string horizontalParamName = "Horizontal";
    public string verticalParamName = "Vertical";

    public string groundParamName = "@Ground";
    public string standParamName = "Stand";
    public string crouchParamName = "Crouch";
    public string crawlParamName = "Crawl";

    public string fireParamName = "Fire";
    public string aimingParamName = "Aiming";
    public string aimingModifierParamName = "AimingModifier";
    public string reloadParamName = "Reload";

    public string airParamName = "@Air";
    public string jumpParamName = "Jump";
    public string fallParamName = "Fall";


    public int RunParamHash { get; private set; }
    public int HorizontalParamHash { get; private set; }
    public int VerticalParamHash { get; private set; }
    
    public int GroundParamHash { get; private set; }
    public int StandParamHash { get; private set; }
    public int CrouchParamHash { get; private set; }
    public int CrawlParamHash { get; private set; }

    public int FireParamHash { get; private set; }
    public int AimingParamHash { get; private set; }
    public int AimingModifierParamHash { get; private set; }
    public int ReloadParamHash { get; private set; }

    public int AirParamHash { get; private set; }
    public int JumpParamHash { get; private set; }
    public int FallParamHash { get; private set; }
    
    
    public void Initialize()
    {
        RunParamHash =          Animator.StringToHash(runParamName);
        HorizontalParamHash =   Animator.StringToHash(horizontalParamName);
        VerticalParamHash =     Animator.StringToHash(verticalParamName);

        GroundParamHash =       Animator.StringToHash(groundParamName);
        StandParamHash =        Animator.StringToHash(standParamName);
        CrouchParamHash =       Animator.StringToHash(crouchParamName);
        CrawlParamHash =        Animator.StringToHash(crawlParamName);

        FireParamHash =         Animator.StringToHash(fireParamName);
        AimingParamHash =       Animator.StringToHash(aimingParamName);
        AimingModifierParamHash = Animator.StringToHash(aimingModifierParamName);
        ReloadParamHash =       Animator.StringToHash(reloadParamName);

        AirParamHash =          Animator.StringToHash(airParamName);
        JumpParamHash =         Animator.StringToHash(jumpParamName);
        FallParamHash =         Animator.StringToHash(fallParamName);
    }
}