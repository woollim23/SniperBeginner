using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseState : MonoBehaviour, IState
{
    protected EnemyStateMachine stateMachine;
    protected readonly EnemyGroundData groundData;

    private Coroutine moveCoroutine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Enemy.Data.GroundData;
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
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        Move();
        moveCoroutine = null;
    }

    private void Move()
    {
        //Vector3 movementDirection = GetMovementDirection();
        //stateMachine.Enemy.Controller.Move(((movementDirection * movementSpeed) + stateMachine.Enemy.ForceReceiver.Movement) * Time.deltaTime);
        //float movementSpeed = GetMovementSpeed();

        //Rotate(GetWanderLocation());
        
        stateMachine.Enemy.agent.SetDestination(GetWanderLocation());
        stateMachine.Enemy.agent.isStopped = false;
    }

    protected void ForceMove()
    {
        stateMachine.Enemy.Controller.Move(stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    }


    void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Enemy.transform.rotation = Quaternion.Lerp(stateMachine.Enemy.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
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

    protected bool IsInChasingRange()
    {
        //if (!stateMachine.Target.GetComponent<PlayerCondition>()) return false;

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRange * stateMachine.Enemy.Data.PlayerChasingRange;
    }

    void WanderToNewLocation()
    {
        // �ݺ������� ���� ��ǥ������ ȣ�����ִ� �Լ�
        stateMachine.Enemy.agent.SetDestination(GetWanderLocation());
        // ��ǥ���� �����ִ� �Լ�
    }

    Vector3 GetWanderLocation()
    {
        // ��ǥ���� �����ִ� �Լ�

        NavMeshHit hit;

        // �������� �˷��ָ� �̵��� �� �ִ� �� �ִܰŸ��� ��ȯ
        // SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask)
        // onUnitSphere : �������� 1�� �� (������ ���� ����)
        // NavMesh.AllAreas : ��� ���� 
        NavMesh.SamplePosition(stateMachine.Enemy.transform.position + (Random.onUnitSphere * Random.Range(stateMachine.Enemy.minWanderDistance, stateMachine.Enemy.maxWanderDistance)), out hit, stateMachine.Enemy.maxWanderDistance, NavMesh.AllAreas);

        

        return hit.position;
    }
}
