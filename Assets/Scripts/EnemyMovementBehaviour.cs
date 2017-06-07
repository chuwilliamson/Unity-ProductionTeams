using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyTargetSelectionBehaviour))]
public class EnemyMovementBehaviour : MonoBehaviour, IDamager
{
    private EnemyTargetSelectionBehaviour SelectTartgetBehaviour;
    [SerializeField]
    public Stats EnemyStats;
    [SerializeField]
    public Transform TargetTower;
    public float DistanceFromTarget;    
    [Tooltip("Distance the enemy must be from target to trigger a state change")]
    public float DistanceToTrigger;
   
    private enum States
    {
        idle, walk, attack
    }

    [SerializeField]
    private States CurrentState;

    public EventEnemyLarvaAttack OnEnemyLarvaAttack;

    void Start()
    {
        SelectTartgetBehaviour = GetComponent<EnemyTargetSelectionBehaviour>();        
        SelectTartgetBehaviour.SearchForTarget();
    }

    public float attackTimer;
    public bool canAttack = true;

    private void Update()
    {
        if(TargetTower == null)
            return;
        if (TargetTower.GetComponent<IDamageable>() == null || CurrentState != States.attack)
            return;

        if (canAttack)
            attackTimer += Time.deltaTime;

        if (attackTimer >= EnemyStats.Items["larvaenemyattackspeed"].Value)
        {
            canAttack = false;            
            OnEnemyLarvaAttack.Invoke(this.gameObject);
        }
    }

    IEnumerator Idle()
    {
        if (TargetTower == null)
            yield return null;
        int LoopCounter = 0;
        CurrentState = States.idle;
        SelectTartgetBehaviour.Agent.isStopped = true;                        
        while (LoopCounter <= 1000)
        {
            if (TargetTower == null)
                continue;
            LoopCounter++;
            DistanceFromTarget = Vector3.Distance(transform.position, SelectTartgetBehaviour.TargetPosition);
            transform.LookAt(TargetTower.position);
            if (DistanceFromTarget > DistanceToTrigger)
            {                
                yield return StartCoroutine("Walk");
            }
            else if(CurrentState != States.attack)
                CurrentState = States.attack;
            yield return null;
        }        
    }

    IEnumerator Walk()
    {
        int LoopCounter = 0;
        CurrentState = States.walk;
        SelectTartgetBehaviour.Agent.isStopped = false;
        while (LoopCounter <= 1000)
        {
            if (TargetTower == null)
                continue;
            LoopCounter++;
            DistanceFromTarget = Vector3.Distance(transform.position, SelectTartgetBehaviour.TargetPosition);
            if (DistanceFromTarget <= DistanceToTrigger)
                yield return StartCoroutine("Idle");
            yield return null;
        }
    }

    public void ChangeTarget(GameObject otherTarget)
    {
        StopAllCoroutines();
        TargetTower = otherTarget.transform;
        StartCoroutine("Idle");
    }

    public void ResetAttackTimer()
    {
        attackTimer = 0;
        canAttack = false;
    }

    public void StartAttackTimer()
    {
        canAttack = true;
    }

    public void Attack()
    {
        if(TargetTower == null)
            return;
        DoDamage(TargetTower.GetComponent<IDamageable>());
    }  

    public void DoDamage(IDamageable target)
    {
        target.TakeDamage(10);
    }

    [System.Serializable]
    public class EventEnemyLarvaAttack : UnityEvent<GameObject>
    {

    }
}
