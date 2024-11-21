using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected readonly EnemyGroundData groundData;

    private Coroutine moveCoroutine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Enemy.Data.GroundData;
        stateMachine.Enemy.Agent.isStopped = false;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
    public virtual void FixedUpdate()
    {
    }

    public virtual void Update()
    {
        stateMachine.Enemy.Agent.speed = groundData.BaseSpeed * (stateMachine.Enemy.isDeadEnemy ? 0 : 1);

        if (moveCoroutine == null)
        {
            moveCoroutine = stateMachine.Enemy.StartCoroutine(DelayedMove());
        }
    }

    private IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(Random.Range(4f, 7f));
        GetWanderLocation();
        moveCoroutine = null;
    }

    private void GetWanderLocation()
    {
        NavMeshHit hit;
        // 포지션을 알려주면 이동할 수 있는 한 최단거리를 반환
        NavMesh.SamplePosition(stateMachine.Enemy.transform.position + (Random.onUnitSphere * Random.Range(groundData.MinWanderDistance, groundData.MaxWanderDistance)), out hit, groundData.MaxWanderDistance, NavMesh.AllAreas);


        Rotate(hit.position);
        stateMachine.Enemy.Agent.SetDestination(hit.position);
    }

    public void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Enemy.transform.rotation = Quaternion.Lerp(stateMachine.Enemy.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected bool IsInChasingRange()
    {
        Vector3 targetPosition = stateMachine.Target.transform.position;
        Vector3 currentPosition = stateMachine.Enemy.transform.position;

        float playerDistanceSqr = (targetPosition - currentPosition).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRange * stateMachine.Enemy.Data.PlayerChasingRange;
    }

    protected bool IsInAttackRange()
    {
        Vector3 targetPosition = stateMachine.Target.transform.position;
        Vector3 currentPosition = stateMachine.Enemy.transform.position;

        float playerDistanceSqr = (targetPosition - currentPosition).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }
}
