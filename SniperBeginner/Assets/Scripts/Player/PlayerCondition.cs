using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public bool IsDead { get; private set; } = false;

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
    public event Action OnDead;

    private void Start() 
    {
        Health = maxHealth;    
    }

    private void OnDisable() 
    {
        OnDead = null;
        OnHealthChanged = null;    
    }

    public void Die()
    {
        IsDead = true;
        OnDead?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(Health - damage, 0f);
    }

    public void Heal(float amount)
    {
        Health = Mathf.Min(Health + amount, maxHealth);
    }

    [ContextMenu("TestDamage")]
    public void TestDamage()
    {
        TakeDamage(50f);
    }
}