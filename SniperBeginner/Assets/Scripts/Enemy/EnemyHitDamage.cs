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
        enemy.health = Mathf.Max(enemy.health - damage, 0);
        if (enemy.health == 0)
            enemy.Die();
    }
}