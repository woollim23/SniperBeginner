using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public string description;
    public Sprite icon;

    [Header("Item Properties")]
    public ItemType itemType;
    public bool isConsumable;

    [Header("Item Effects")]
    public int healAmount;
    public float speedMultiplier;
    public bool addsSilencer;
}