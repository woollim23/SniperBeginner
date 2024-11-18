using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public bool IsDead { get; private set; }

    // 체력
    [SerializeField] float health;
    public float Health
    {
        get => health;
        set 
        {
            if (IsDead) 
                return;

            health = value;
            OnHealthChanged?.Invoke(health / maxHealth);

            if(health <= 0f)
                Die();
        }
    }
    [SerializeField] float maxHealth = 100f;

    public event Action<float> OnHealthChanged;

    private void Start() 
    {
        Health = maxHealth;    
    }

    public void Die()
    {
        IsDead = true;
    }

    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(Health - damage, 0f);
    }

    [ContextMenu("TestDamage")]
    public void TestDamage()
    {
        TakeDamage(50f);
    }
}