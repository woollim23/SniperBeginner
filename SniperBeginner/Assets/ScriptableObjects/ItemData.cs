using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public int minValue;
    public int maxValue;
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;

    [Header("Effect Properties")]
    public ItemEffect effect;

    [Header("Prefab")]
    public GameObject dropPrefab;

    public GeneratedItem GenerateRandomItem()
    {
        if (effect.minValue > effect.maxValue)
        {
            return null;
        }

        int effectValue = Random.Range(effect.minValue, effect.maxValue + 1);
        return new GeneratedItem(itemType, effectValue);
    }

    public GameObject GetDropPrefab()
    {
        return dropPrefab;
    }
}

[System.Serializable]
public class GeneratedItem
{
    public ItemType itemType;
    public int effectValue;

    public GeneratedItem(ItemType itemType, int effectValue)
    {
        this.itemType = itemType;
        this.effectValue = effectValue;
    }
}