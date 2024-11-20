using UnityEngine;

public class EnemyHitDamage : MonoBehaviour, IDamagable, ISnipable
{
    [SerializeField] private float damageModifier = 1;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void TakeDamage(float damage)
    {
        enemy.OnTakeDamage(damage * damageModifier);
    }

    public bool IsSnipable(float damage)
    {
        return enemy.Health <= damage * damageModifier;
    }
}