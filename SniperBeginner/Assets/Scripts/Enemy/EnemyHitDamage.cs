using UnityEngine;

public abstract class HitDamage : MonoBehaviour, IDamagable
{
    public abstract void TakeDamage(float damage);

}

public class EnemyHitDamage : HitDamage, ISnipable
{
    [SerializeField] private float damageModifier = 1;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public override void TakeDamage(float damage)
    {
        enemy.OnTakeDamage(damage * damageModifier);
    }

    public bool IsSnipable(float damage)
    {
        return enemy.Health > 0f && enemy.Health <= damage * damageModifier;
    }
}