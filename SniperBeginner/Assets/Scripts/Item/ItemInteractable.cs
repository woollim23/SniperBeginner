using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable
{
    private GeneratedItem generatedItem;

    public void Initialize(GeneratedItem item)
    {
        if (item == null)
        {
            return;
        }

        generatedItem = item;
    }

    public InteractionData GetInformation()
    {
        if (generatedItem == null)
        {
            return null;
        }

        string description = generatedItem.itemType == ItemType.Heal
            ? $"체력을 {generatedItem.effectValue} 회복합니다."
            : $"탄약 {generatedItem.effectValue}발로 교체합니다.";

        return new InteractionData(InterationType.Pick)
        {
            interactionName = generatedItem.itemType.ToString(),
            description = description,
            interactKey = KeyCode.F
        };
    }

    public void Interact()
    {
        if (generatedItem == null)
        {
            return;
        }

        ApplyEffect(CharacterManager.Instance.Player);

        Destroy(gameObject);
    }

    private void ApplyEffect(Player player)
    {
        if (player == null || generatedItem == null)
        {
            return;
        }

        switch (generatedItem.itemType)
        {
            case ItemType.Heal:
                CharacterManager.Instance.Player.Condition.Heal(generatedItem.effectValue);
                break;

            case ItemType.RifleMagazine:
                CharacterManager.Instance.Player.Equipment.ReplaceAmmo(generatedItem.effectValue, AmmoType.RifleAmmo); 
                break;

            case ItemType.SniperMagazine:
                CharacterManager.Instance.Player.Equipment.ReplaceAmmo(generatedItem.effectValue, AmmoType.SniperAmmo);
                break;

            default:
                break;
        }
    }
}