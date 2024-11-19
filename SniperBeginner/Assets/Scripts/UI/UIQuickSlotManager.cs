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
            if (i < allWeapons.Count) // ���Ⱑ ���� ������ ���� ��
            {
                quickSlots[i].weaponData = allWeapons[i];
                quickSlots[i].UpdateUI(false); // �ʱ⿡�� ���õ��� ���� ����
            }
            else
            {
                quickSlots[i].weaponData = null;
                quickSlots[i].UpdateUI(false); // ���� ��Ȱ��ȭ
            }
        }
    }

    // ������ ���� ó��
    private void HandleQuickSlotSelection(int slotIndex)
    {
        if (slotIndex < 1 || slotIndex > quickSlots.Count)
        {
            return;
        }

        int index = slotIndex - 1;

        // ��� �ִ� �������� Ȯ��
        if (quickSlots[index].weaponData == null)
        {
            return;
        }

        // ���� ���� ��Ȱ��ȭ
        if (currentSlotIndex >= 0 && currentSlotIndex < quickSlots.Count)
        {
            quickSlots[currentSlotIndex].UpdateUI(false);
        }

        // ���ο� ���� Ȱ��ȭ
        currentSlotIndex = index;
        quickSlots[currentSlotIndex].UpdateUI(true);

        WeaponData selectedWeapon = quickSlots[currentSlotIndex].weaponData;
        if (OnWeaponSelected != null)
        {
            OnWeaponSelected(selectedWeapon);
        }
        else
        {
            Debug.LogWarning("OnWeaponSelected �̺�Ʈ�� �����ڰ� �����ϴ�.");
        }
    }
}