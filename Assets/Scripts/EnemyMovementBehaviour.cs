using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementBehaviour : MonoBehaviour
{
    private NavMeshAgent _NavMeshAgent;
    [SerializeField]
    private Transform TargetTower;
    public float MovementSpeed;
    public float DistanceFromTarget;

    enum States
    {
        idle, walk, attack, death
    }

    [SerializeField]
    private States CurrentState;

    void Start()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
        TargetTower = SearchForClosestTower();
        _NavMeshAgent.destination = TargetTower.position;
        Debug.Log(TargetTower.position);
        _NavMeshAgent.speed = MovementSpeed;
        StartCoroutine("Idle");
    }

    void Update()
    {
        DistanceFromTarget = Vector3.Distance(transform.position, TargetTower.position);
    }

    IEnumerator Idle()
    {
        int LoopCounter = 0;
        CurrentState = States.idle;
        while (LoopCounter <= 1000)
        {
            LoopCounter++;            
            if (DistanceFromTarget > 3.0f)
                yield return StartCoroutine("Walk");
            yield return null;
        }        
    }

    IEnumerator Walk()
    {
        int LoopCounter = 0;
        CurrentState = States.walk;
        TargetTower = SearchForClosestTower();
        _NavMeshAgent.destination = TargetTower.position;
        while (LoopCounter <= 1000)
        {
            LoopCounter++;            
            if (DistanceFromTarget <= 3.0f)
                yield return StartCoroutine("Attack");
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        int LoopCounter = 0;
        CurrentState = States.attack;
        int attackTimer = 0;
        while (LoopCounter <= 1000)
        {
            LoopCounter++;
            attackTimer++;
            if (attackTimer >= 3)
            {
                yield return StartCoroutine("Walk");
            }
            yield return null;
        }
    }

    Transform SearchForClosestTower()
    {
        var towers = GameObject.FindGameObjectsWithTag("PlayerTower").ToList();
        if (towers.Count == 1)
        {
            return towers[0].transform;
        }
        GameObject closestTower;
        if (TargetTower)
        {
            if (towers.IndexOf(TargetTower.gameObject) == 0)
                closestTower = towers[1];
            else
                closestTower = towers[0];
        }
        else        
            closestTower = towers[0];        
        var closestTowerDistance = Vector3.Distance(transform.position, towers[0].transform.position);        
        foreach (var tower in towers)
        {
            if (tower.transform == TargetTower)
                continue;
            if (Vector3.Distance(transform.position, tower.transform.position) < closestTowerDistance)
                closestTower = tower;
        }
        return closestTower.transform;
    }
}
