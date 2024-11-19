using UnityEngine;

public class EnemyHitDamage : MonoBehaviour, IDamagable
{
    [SerializeField] private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public virtual void TakeDamage(float damage)
    {
        enemy.onTakeDamage(damage);
    }

}