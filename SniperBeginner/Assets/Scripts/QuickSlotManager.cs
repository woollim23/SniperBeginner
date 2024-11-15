using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotManager : MonoBehaviour
{
    public Image[] slotImages;
    public Text[] quantityTexts;
    public GameObject[] equipTexts;

    public List<ItemData> quickSlots = new List<ItemData>();
    public List<int> itemQuantities = new List<int>();

    public void OnSlotSelected(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < quickSlots.Count && quickSlots[slotIndex] != null)
        {
            ItemData item = quickSlots[slotIndex];
            ApplyItemEffect(item);

            if (item.isConsumable)
            {
                itemQuantities[slotIndex]--;
                if (itemQuantities[slotIndex] <= 0)
                {
                    RemoveItem(slotIndex);
                }
            }

            UpdateQuickSlotUI();
        }
    }

    void ApplyItemEffect(ItemData item)
    {
        switch (item.itemType)
        {
            case ItemType.Heal:
                Debug.Log($"Healed by {item.healAmount} HP!");
                break;
            case ItemType.SpeedBoost:
                Debug.Log($"Speed increased by {item.speedMultiplier}x!");
                break;
            case ItemType.Silencer:
                Debug.Log("Silencer attached!");
                break;
        }
    }

    void RemoveItem(int slotIndex)
    {
        quickSlots[slotIndex] = null;
        itemQuantities[slotIndex] = 0;

        for (int i = slotIndex; i < quickSlots.Count - 1; i++)
        {
            quickSlots[i] = quickSlots[i + 1];
            itemQuantities[i] = itemQuantities[i + 1];
        }

        quickSlots[quickSlots.Count - 1] = null;
        itemQuantities[quickSlots.Count - 1] = 0;
    }

    void UpdateQuickSlotUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < quickSlots.Count && quickSlots[i] != null)
            {
                slotImages[i].sprite = quickSlots[i].icon;
                slotImages[i].enabled = true;
                quantityTexts[i].text = itemQuantities[i].ToString();
                quantityTexts[i].enabled = true;
                equipTexts[i].SetActive(false);
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].enabled = false;
                quantityTexts[i].text = "";
                quantityTexts[i].enabled = false;
                equipTexts[i].SetActive(false);
            }
        }
    }

    public void SwapSlots(int fromIndex, int toIndex)
    {
        if (fromIndex >= 0 && fromIndex < quickSlots.Count && toIndex >= 0 && toIndex < quickSlots.Count)
        {
            (quickSlots[fromIndex], quickSlots[toIndex]) = (quickSlots[toIndex], quickSlots[fromIndex]);
            (itemQuantities[fromIndex], itemQuantities[toIndex]) = (itemQuantities[toIndex], itemQuantities[fromIndex]);

            UpdateQuickSlotUI();
        }
    }
}