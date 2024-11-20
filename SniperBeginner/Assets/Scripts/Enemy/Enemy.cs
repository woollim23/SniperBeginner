using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field: Header("Enemy Data")]
    [field: SerializeField] private float health;
    public float Health {get => health;}

    public NavMeshAgent Agent { get; private set; }
    public float MinWanderDistance { get; private set; }
    public float MaxWanderDistance { get; private set; }
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
        MinWanderDistance = 2;
        MaxWanderDistance = 5;
        Agent = GetComponent<NavMeshAgent>();
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
        Agent.isStopped = true;
        GameManager.Instance.CountDeadEnemy();
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

}
