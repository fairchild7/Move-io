using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    public IState currentState;

    private Vector3 destination;

    public RectTransform arrowPrefab;

    private void Awake()
    {
        arrowPrefab = Instantiate(arrowPrefab);   
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnInit()
    {
        UIGamePlay.Instance.SetParentToThis(this);
        base.OnInit();
        agent.isStopped = false;
        ChangeState(new MoveState());
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void SetDestination(Vector3 target)
    {
        this.destination = target;
    }

    public Vector3 GetDestination()
    {
        return this.destination;
    }

    public float GetDistance(Vector3 target)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(transform.position.x - target.x), 2f) + Mathf.Pow(Mathf.Abs(transform.position.z - target.z), 2f));
    }

    public void RandomMove()
    {
        agent.isStopped = false;
        Vector3 target = new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f));
        agent.SetDestination(target);
        SetDestination(target);
        ChangeAnimatorParameter("IsIdle", false);
        numBullet = 1;
    }

    public void StopMove()
    {
        agent.isStopped = true;
        ChangeAnimatorParameter("IsIdle", true);
    }

    public override void Attack()
    {
        StopMove();
        base.Attack();
    }

    protected override void Dead()
    {
        agent.isStopped = true;
        base.Dead();
        Invoke(nameof(Despawn), 1f);
    }

    private void Despawn()
    {
        EnemyPool.Instance.Despawn(this);
    }
}
