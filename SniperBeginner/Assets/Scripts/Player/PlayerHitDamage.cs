using UnityEngine;

public class PlayerHitDamage : HitDamage
{
    [SerializeField] private float damageModifier = 1;
    private PlayerCondition condition;

    private void Start()
    {
        condition = CharacterManager.Instance.Player.Condition;
    }

    public override void TakeDamage(float damage)
    {
        condition.TakeDamage(damage * damageModifier);
    }
}