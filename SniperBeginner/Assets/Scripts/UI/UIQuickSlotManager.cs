using System.Collections.Generic;
using UnityEngine;
using System;

public class QuickSlotManager : MonoBehaviour
{
    [Header("Quick Slots")]
    public Transform slotParent;
    public List<UIQuickSlot> quickSlots = new List<UIQuickSlot>();
    public List<WeaponData> allWeapons = new List<WeaponData>();

    public event Action<WeaponData> OnWeaponSelected;

    private int currentSlotIndex = -1;

    private void Start()
    {
        AutoRegisterSlots();
        InitializeQuickSlots();

        InputManager.Instance.OnQuickSlotEvent += HandleQuickSlotSelection;
    }

    private void AutoRegisterSlots()
    {
        quickSlots.Clear();

        foreach (Transform child in slotParent)
        {
            UIQuickSlot quickSlot = child.GetComponent<UIQuickSlot>();
            if (quickSlot != null)
            {
                quickSlots.Add(quickSlot);
            }
        }
    }

    private void InitializeQuickSlots()
    {
        for (int i = 0; i < quickSlots.Count; i++)
        {
            if (i < allWeapons.Count) // 무기가 슬롯 수보다 적을 때
            {
                quickSlots[i].weaponData = allWeapons[i];
                quickSlots[i].UpdateUI(false); // 초기에는 선택되지 않은 상태
            }
            else
            {
                quickSlots[i].weaponData = null;
                quickSlots[i].UpdateUI(false); // 슬롯 비활성화
            }
        }
    }

    // 퀵슬롯 선택 처리
    private void HandleQuickSlotSelection(int slotIndex)
    {
        if (slotIndex < 1 || slotIndex > quickSlots.Count)
        {
            return;
        }

        int index = slotIndex - 1;

        // 비어 있는 슬롯인지 확인
        if (quickSlots[index].weaponData == null)
        {
            return;
        }

        // 이전 슬롯 비활성화
        if (currentSlotIndex >= 0 && currentSlotIndex < quickSlots.Count)
        {
            quickSlots[currentSlotIndex].UpdateUI(false);
        }

        // 새로운 슬롯 활성화
        currentSlotIndex = index;
        quickSlots[currentSlotIndex].UpdateUI(true);

        WeaponData selectedWeapon = quickSlots[currentSlotIndex].weaponData;
        if (OnWeaponSelected != null)
        {
            OnWeaponSelected(selectedWeapon);
        }
        else
        {
            Debug.LogWarning("OnWeaponSelected 이벤트에 구독자가 없습니다.");
        }
    }
}