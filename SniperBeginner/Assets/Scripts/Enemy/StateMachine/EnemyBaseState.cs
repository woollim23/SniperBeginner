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
        if (moveCoroutine == null)
        {
            moveCoroutine = stateMachine.Enemy.StartCoroutine(DelayedMove());
        }
    }

    private IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(Random.Range(4f, 7f));
        Move();
        moveCoroutine = null;
    }


    private void Move()
    {
        // MovementSpeedModifier에 따라 속력 변경
        // 체력으로 죽음 확인 후, 움직임 결정

        stateMachine.Enemy.Agent.speed = stateMachine.BaseSpeed * stateMachine.MovementSpeedModifier;

        stateMachine.Enemy.Agent.SetDestination(GetWanderLocation());
    }

    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        // 포지션을 알려주면 이동할 수 있는 한 최단거리를 반환
        NavMesh.SamplePosition(stateMachine.Enemy.transform.position + (Random.onUnitSphere * Random.Range(stateMachine.Enemy.MinWanderDistance, stateMachine.Enemy.MaxWanderDistance)), out hit, stateMachine.Enemy.MaxWanderDistance, NavMesh.AllAreas);


        Rotate(hit.position);

        return hit.position;
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
        //if (!stateMachine.Target.GetComponent<PlayerCondition>()) return false;

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRange * stateMachine.Enemy.Data.PlayerChasingRange;
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }

    protected void ChangeWarningState()
    {
        //stateMachine.ChasingState(Warn)
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
