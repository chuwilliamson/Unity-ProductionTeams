using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementBehaviour : MonoBehaviour, IDamager
{
    private NavMeshAgent _NavMeshAgent;
    [SerializeField]
    public Stats EnemyStats;
    [SerializeField]
    private Transform TargetTower;
    public float MovementSpeed;
    public float DistanceFromTarget;
    public bool CanWalk;
    [Tooltip("Distance the enemy must be from target to trigger a state change")]
    public float DistanceToTrgger;
    private enum States
    {
        idle, walk, attack
    }

    [SerializeField]
    private States CurrentState;

    public EventEnemyLarvaAttack OnEnemyLarvaAttack;

    void Start()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();        
        TargetTower = GameObject.FindGameObjectWithTag("PlayerTower").transform;
        _NavMeshAgent.destination = TargetTower.position;        
        _NavMeshAgent.speed = MovementSpeed;
        StartCoroutine("Idle");
    }

    void OnDrawGizmos()
    {
        if(_NavMeshAgent)
            Gizmos.DrawLine(transform.position, _NavMeshAgent.destination);
    }

    IEnumerator Idle()
    {
        int LoopCounter = 0;
        CurrentState = States.idle;
        _NavMeshAgent.isStopped = true;                        
        while (LoopCounter <= 1000)
        {
            LoopCounter++;
            DistanceFromTarget = Vector3.Distance(transform.position, TargetTower.position);
            transform.LookAt(TargetTower.position);
            if (DistanceFromTarget > DistanceToTrgger)
            {
                StopCoroutine("Attack");
                yield return StartCoroutine("Walk");
            }
            else if(CurrentState != States.attack)
                StartCoroutine("Attack");           
            yield return null;
        }        
    }

    IEnumerator Walk()
    {
        int LoopCounter = 0;
        CurrentState = States.walk;
        _NavMeshAgent.isStopped = false;
        while (LoopCounter <= 1000)
        {
            LoopCounter++;
            DistanceFromTarget = Vector3.Distance(transform.position, TargetTower.position);
            if (DistanceFromTarget <= DistanceToTrgger)
                yield return StartCoroutine("Idle");
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        int LoopCounter = 0;
        CurrentState = States.attack;
        while (LoopCounter <= 1000)
        {
            if (TargetTower.GetComponent<IDamageable>() != null)
            {                
                TargetTower.GetComponent<IDamageable>().TakeDamage(10);
                OnEnemyLarvaAttack.Invoke(this.gameObject);
                yield return new WaitForSeconds(EnemyStats.Items["larvaenemyattackspeed"].Value);
            }
        }
    }

    [System.Serializable]
    public class EventEnemyLarvaAttack : UnityEvent<GameObject>
    {
        
    }

    public void DoDamage(IDamageable target)
    {
        target.TakeDamage(10);
    }
}
