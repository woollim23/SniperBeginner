using UnityEngine;

public class ItemDropManager : Singleton<ItemDropManager>
{
    [Header("Item Data")]
    [SerializeField] private ItemData healItemData;
    [SerializeField] private ItemData pistolAmmoData;
    [SerializeField] private ItemData sniperAmmoData;

    [Header("Drop Rates")]
    [Range(0, 1f)] public float healDropRate = 0.4f;
    [Range(0, 1f)] public float pistolAmmoDropRate = 0.35f;
    [Range(0, 1f)] public float sniperAmmoDropRate = 0.25f;

    public void DropRandomItem(Vector3 position)
    {
        ItemData selectedItem = SelectRandomItem();
        if (selectedItem == null) return;

        GeneratedItem randomItem = selectedItem.GenerateRandomItem();

        GameObject droppedItem = Instantiate(selectedItem.dropPrefab, position, Quaternion.identity);

        var interactable = droppedItem.GetComponent<ItemInteractable>();
        if (interactable != null)
        {
            interactable.Initialize(randomItem);
        }
    }

    private ItemData SelectRandomItem()
    {
        float totalRate = healDropRate + pistolAmmoDropRate + sniperAmmoDropRate;
        if (totalRate <= 0)
        {
            return null;
        }

        float randomValue = Random.Range(0, totalRate);

        if (randomValue < healDropRate)
            return healItemData;
        else if (randomValue < healDropRate + pistolAmmoDropRate)
            return pistolAmmoData;
        else
            return sniperAmmoData;
    }
}