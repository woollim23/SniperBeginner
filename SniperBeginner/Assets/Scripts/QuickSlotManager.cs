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

    // ���� ���� �� ó��
    public void OnSlotSelected(int slotIndex)
    {
        if (IsValidSlotIndex(slotIndex) && quickSlots[slotIndex] != null)
        {
            WeaponData selectedWeapon = quickSlots[slotIndex];

            // ���� ����
            //playerEquipment.Equip(selectedWeapon);

            // UI ����
            UpdateQuickSlotUI();
        }
    }

    // ���Կ� ������ ���⸦ UI�� ������Ʈ
    void UpdateQuickSlotUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < quickSlots.Count && quickSlots[i] != null)
            {
                slotImages[i].enabled = true;
                slotImages[i].sprite = quickSlots[i].icon; // ������ ������
                quantityTexts[i].enabled = false; // ������ ������� ����
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

    // ���� ��ü (���� ��ü)
    public void SwapSlots(int fromIndex, int toIndex)
    {
        if (IsValidSlotIndex(fromIndex) && IsValidSlotIndex(toIndex))
        {
            (quickSlots[fromIndex], quickSlots[toIndex]) = (quickSlots[toIndex], quickSlots[fromIndex]);

            UpdateQuickSlotUI();
        }
    }

    // ��ȿ�� ���� �ε������� Ȯ��
    private bool IsValidSlotIndex(int index)
    {
        return index >= 0 && index < quickSlots.Count;
    }
}