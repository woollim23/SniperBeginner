using System.Collections.Generic;
using UnityEngine;
using System;

public class UIQuickSlotManager : MonoBehaviour
{
    PlayerEquipment equipment;

    [Header("Quick Slots")]
    public Transform slotParent; // UI
    public List<UIQuickSlot> quickSlots = new List<UIQuickSlot>(); // UI

    private int currentSlotIndex = -1; // 로직

    private void Awake()
    {
        equipment = CharacterManager.Instance.Player.Equipment;

        AutoRegisterSlots();
        InitializeQuickSlots();

        InputManager.Instance.OnQuickSlotEvent += HandleQuickSlotSelection;
    }

    private void Start() 
    {
        HandleQuickSlotSelection(1);
    }

    // UI
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

    // 데이터 로직 -> UI 업데이트
    private void InitializeQuickSlots()
    {
        for (int i = 0; i < quickSlots.Count; i++)
        {
            if (i < equipment.allWeapons.Count)
            {
                quickSlots[i].weaponData = equipment.allWeapons[i];
                quickSlots[i].UpdateUI(false);
            }
            else
            {
                quickSlots[i].weaponData = null;
                quickSlots[i].UpdateUI(false);
            }
        }
    }

    
    // 데이터 로직 -> UI 업데이트
    public void HandleQuickSlotSelection(int slotIndex)
    {
        // 결합이 강력해짐
        if(CharacterManager.Instance.Player.Shooting.isAiming) return;

        if (slotIndex < 1 || slotIndex > quickSlots.Count)
        {
            return;
        }

        int index = slotIndex - 1;

        if (quickSlots[index].weaponData == null)
        {
            return;
        }

        if (currentSlotIndex >= 0 && currentSlotIndex < quickSlots.Count)
        {
            quickSlots[currentSlotIndex].UpdateUI(false);
        }

        currentSlotIndex = index;
        quickSlots[currentSlotIndex].UpdateUI(true);

        WeaponData selectedWeapon = quickSlots[currentSlotIndex].weaponData;
        if (selectedWeapon != null)
        {
            PlayEquipSound(selectedWeapon);
        }

        equipment.WeaponSelected(slotIndex);
    }

    private void PlayEquipSound(WeaponData weaponData)
    {
        if (weaponData != null && weaponData.equipSound != null)
        {
            SoundManager.Instance.PlaySound(weaponData.equipSound);
        }        
    }
}