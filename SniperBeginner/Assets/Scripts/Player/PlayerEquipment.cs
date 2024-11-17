using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    // TODO : 플레이어 장착에 대한 정보 -> Equipment나 Gun 스크립트가 필요할 듯
    [field:SerializeField] public DummyWeapon CurrentEquip { get; private set; }

    // 손 위치
    public Transform leftHand;
    public Transform rightHand;

    private void Start() 
    {
        // 테스트용
        Equip(CurrentEquip);
    }

    private void FixedUpdate() 
    {
        if(CurrentEquip)
            CurrentEquip.transform.rotation = Quaternion.LookRotation(leftHand.position - rightHand.position, Vector3.up);
    }

    public void Equip(DummyWeapon equipment)
    {
        equipment.transform.SetParent(rightHand);
        equipment.transform.localPosition = Vector3.zero;

        // 장착했다 -> 쓸 수 있다는 것
        // 무기라면 투사체 오브젝트 풀링이 확인해줄 것
        if (!ObjectPoolManager.Instance.projectilePools.ContainsKey(CurrentEquip.projectile.data.type))
        {
            ObjectPoolManager.Instance.AddProjectilePool(CurrentEquip.projectile);
        }
    }

    // 뼈에서 손 위치만 찾는 메서드
    [ContextMenu("Find Hand")]
    public void FindHand()
    {
        Animator anim = GetComponent<Animator>();

        if (leftHand == null)
            leftHand = new GameObject("Left Hand").transform;
        
        if (rightHand == null)
            rightHand = new GameObject("Right Hand").transform;


        leftHand.SetParent(anim.GetBoneTransform(HumanBodyBones.LeftHand));
        rightHand.SetParent(anim.GetBoneTransform(HumanBodyBones.RightHand));

        leftHand.localPosition = Vector3.zero;
        leftHand.localEulerAngles = Vector3.zero;

        rightHand.localPosition = Vector3.zero;
        rightHand.localEulerAngles = Vector3.zero;
    }
}

