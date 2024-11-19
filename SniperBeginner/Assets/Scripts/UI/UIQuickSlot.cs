using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIQuickSlot : MonoBehaviour
{
    public WeaponData weaponData;
    public Image slotImage;
    public GameObject equipText;
    public Image background;

    private Color defaultColor = new Color32(23, 24, 30, 255);
    private Color selectedColor = new Color32(1, 55, 75, 255);

    public void UpdateUI(bool isSelected)
    {
        if (slotImage == null || equipText == null || background == null)
        {
            Debug.LogWarning("UIQuickSlot: ���� UI ��Ұ� ������� �ʾҽ��ϴ�.");
            return;
        }

        if (weaponData != null)
        {
            slotImage.enabled = true;
            slotImage.sprite = weaponData.icon;

            background.color = isSelected ? selectedColor : defaultColor;

            equipText.SetActive(isSelected);
        }
        else
        {
            slotImage.enabled = false;
            equipText.SetActive(false);
            background.color = defaultColor;
        }
    }
}
