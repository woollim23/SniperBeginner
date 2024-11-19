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
            ? $"ü���� {generatedItem.effectValue} ȸ���մϴ�."
            : $"ź�� {generatedItem.effectValue}�߷� ��ü�մϴ�.";

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
                //CharacterManager.Instance.Player.AddHeal(generatedItem.effectValue); // ü�� ȸ�� ����
                break;

            case ItemType.PistolMagazine:
                //CharacterManager.Instance.Player.ReplaceAmmo(generatedItem.effectValue, AmmoType.PistolAmmo); // ���� ź�� ��ü ����
                break;

            case ItemType.SniperMagazine:
                //CharacterManager.Instance.Player.ReplaceAmmo(generatedItem.effectValue, AmmoType.SniperAmmo); // ���� ź�� ��ü ����
                break;

            default:
                break;
        }
    }
}