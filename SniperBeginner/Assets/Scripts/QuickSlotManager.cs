using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Image[] slotImages;
    public Text[] quantityTexts;
    public GameObject[] equipTexts;

    [Header("Quick Slot Data")]
    public List<WeaponData> quickSlots = new List<WeaponData>();
    public List<int> itemQuantities = new List<int>();

    private PlayerEquipment playerEquipment;

    private void Start()
    {
        playerEquipment = GetComponent<PlayerEquipment>();
    }

    // 슬롯 선택 시 처리
    public void OnSlotSelected(int slotIndex)
    {
        if (IsValidSlotIndex(slotIndex) && quickSlots[slotIndex] != null)
        {
            WeaponData selectedWeapon = quickSlots[slotIndex];

            // 무기 장착
            //playerEquipment.Equip(selectedWeapon);

            // UI 갱신
            UpdateQuickSlotUI();
        }
    }

    // 슬롯에 장착된 무기를 UI에 업데이트
    void UpdateQuickSlotUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < quickSlots.Count && quickSlots[i] != null)
            {
                slotImages[i].enabled = true;
                slotImages[i].sprite = quickSlots[i].icon; // 무기의 아이콘
                quantityTexts[i].enabled = false; // 수량은 사용하지 않음
                equipTexts[i].SetActive(false);
            }
            else
            {
                slotImages[i].enabled = false;
                quantityTexts[i].enabled = false;
                equipTexts[i].SetActive(false);
            }
        }
    }

    // 슬롯 교체 (무기 교체)
    public void SwapSlots(int fromIndex, int toIndex)
    {
        if (IsValidSlotIndex(fromIndex) && IsValidSlotIndex(toIndex))
        {
            (quickSlots[fromIndex], quickSlots[toIndex]) = (quickSlots[toIndex], quickSlots[fromIndex]);

            UpdateQuickSlotUI();
        }
    }

    // 유효한 슬롯 인덱스인지 확인
    private bool IsValidSlotIndex(int index)
    {
        return index >= 0 && index < quickSlots.Count;
    }
}