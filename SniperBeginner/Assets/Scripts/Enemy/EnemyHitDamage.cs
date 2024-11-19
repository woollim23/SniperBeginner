using UnityEngine;

public class EnemyHitDamage : MonoBehaviour, IDamagable, ISnipable
{
    [SerializeField] protected Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public virtual void TakeDamage(float damage)
    {
        enemy.onTakeDamage(damage);
    }

    public virtual bool IsSnipable(float damage)
    {
        return enemy.Health <= damage;
    }
}