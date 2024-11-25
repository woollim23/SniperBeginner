using System;
using UnityEngine;

// TODO : 리팩토링 : 적 체력과 관련해서 통합 가능한지
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public bool IsDead { get; private set; } = false;

    // 체력
    [SerializeField] float health = 100f;
    public float Health
    {
        get => health;
        set 
        {
            if (IsDead) 
                return;

            health = value;

            OnHealthChanged?.Invoke(GetPercent());

            if(health <= 0f)
                Die();
        }
    }
    [SerializeField] float maxHealth = 100f;

    public event Action<float> OnHealthChanged;
    public event Action OnDead;

    private void OnDisable() 
    {
        OnDead = null;
        OnHealthChanged = null;    
    }

    public void Initialize(float initialHealth = 0f)
    {
        if (initialHealth > 0f)
            Health = initialHealth;
        else
            Health = maxHealth;
    }

    public void Die()
    {
        IsDead = true;
        OnDead?.Invoke();

        UIManager.Instance.OpenGameOverMenu();

        SoundManager.Instance.PlaySound(SoundManager.Instance.playerDeadSFX, 1.0f);
    }

    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(Health - damage, 0f);
    }

    public void Heal(float amount)
    {
        Health = Mathf.Min(Health + amount, maxHealth);
    }

    public float GetPercent()
    {
        return Health / maxHealth;
    }

    [ContextMenu("TestDamage")]
    public void TestDamage()
    {
        TakeDamage(50f);
    }
}