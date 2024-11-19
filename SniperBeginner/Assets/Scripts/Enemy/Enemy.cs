using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour, ISnipable
{
    [field: Header("Enemy Data")]
    [field: SerializeField] public float health;

    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }

    [field: Header("DropItems")]
    [field: SerializeField] public ItemData[] dropOnDeath; // ?????? ?????? ?????? ?è¿?
    [field: SerializeField] private Transform dropPosition;

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    public EnemyStateMachine stateMachine;

    //[field: SerializeField] public Weapon Weapon { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();

        stateMachine = new EnemyStateMachine(this);
        health = 100; // TODO : ??? ??? ????

        EnemyDatalInit();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
        if (health < 0)
            Die();
    }

    public void EnemyDatalInit()
    {
        health = Data.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);

        Animator.SetTrigger("Hit");
        if (health == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GiveItem();
        Animator.avatar = null;

        GameManager.Instance.CountDeadEnemy();
        // TODO : 5ÃÊµÚ ¿¡³Ê¹Ì ÆÄ±«
    }

    void GiveItem()
    {
        ItemDropManager.Instance.DropRandomItem(dropPosition.position);
    }

    public float CheckRemainHealth()
    {
        return 1f; // ?„ì‹œë¡?1f ë°˜í™˜
        // ?ëž˜???„ìž¬ ?¨ì? ì²´ë ¥??ì¤˜ì•¼??
    }
}
