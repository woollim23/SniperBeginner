using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour, ISnipable
{
    [field: Header("Enemy Data")]
    [field: SerializeField] private float health;

    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }

    [field: Header("DropItems")]
    [field: SerializeField] public ItemData[] dropOnDeath;
    [field: SerializeField] private Transform dropPosition;

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    public EnemyStateMachine stateMachine;

    public Action<float> onTakeDamage;

    //[field: SerializeField] public Weapon Weapon { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();

        ForceReceiver = GetComponent<ForceReceiver>();
        stateMachine = new EnemyStateMachine(this);

        onTakeDamage += OnTakeDamage;

        EnemyDatalInit();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
        if (health <= 0) Die();
    }

    public void EnemyDatalInit()
    {
        health = Data.MaxHealth;
    }

    public void OnTakeDamage(float damage)
    {
        Animator.SetTrigger("Hit");

        health = Mathf.Max(health - damage, 0);
        if (health == 0)
            Die();
    }

    public void Die()
    {
        GiveItem();
        Animator.avatar = null;

        GameManager.Instance.CountDeadEnemy();
        // TODO : 5ÃÊµÚ ¿¡³Ê¹Ì ÆÄ±«
        Invoke("DestroyEnemy", 5);
    }

    private void GiveItem()
    {
        ItemDropManager.Instance.DropRandomItem(dropPosition.position);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public float CheckRemainHealth()
    {
        return 1f; // ?„ì‹œë¡?1f ë°˜í™˜
        // ?ëž˜???„ìž¬ ?¨ì? ì²´ë ¥??ì¤˜ì•¼??
    }
}
