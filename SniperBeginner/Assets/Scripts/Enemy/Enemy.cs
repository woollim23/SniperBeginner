using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field: Header("Enemy Data")]
    [field: SerializeField] private float health;
    public float Health {get => health;}
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
    public NavMeshAgent Agent { get; private set; }

    public Action<float> onTakeDamage;

    //[field: SerializeField] public Weapon Weapon { get; private set; }

    private void Awake()
    {
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
        health = Mathf.Max(health - damage, 0);
        if (health == 0)
        {
            Die();
            return;
        }

        Animator.SetTrigger("Hit");
        Agent.isStopped = true;
        StartCoroutine(WaitForHitAnimation());
    }

    private IEnumerator WaitForHitAnimation()
    {
        AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        while (stateInfo.IsName("Hit") && stateInfo.normalizedTime < 1f)
        {
            yield return null;
            stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        }

        Agent.isStopped = false;
    }

    public void Die()
    {
        GiveItem();
        Animator.enabled = false;
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
