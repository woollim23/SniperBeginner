using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    // 플레이어 장착에 대한 정보
    [field:SerializeField] public GameObject CurrentEquip { get; private set; }

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

    public void Equip(GameObject equipment)
    {
        equipment.transform.SetParent(rightHand);
        equipment.transform.localPosition = Vector3.zero;
    }

    // 뼈에서 손 위치만 찾는 메서드
    // 런타임용 아님
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

