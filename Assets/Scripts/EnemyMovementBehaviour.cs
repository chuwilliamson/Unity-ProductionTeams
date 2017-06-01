using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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

    public EventEnemyLarvaAttack OnEnemyLarvaAttack;

    void Awake()
    {
        EnemyFormationBehaviour.OnUnitStop.AddListener(StopMovement);
    }

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
        _NavMeshAgent.destination = transform.position;
        _NavMeshAgent.speed = 0;        
        while (LoopCounter <= 1000)
        {
            LoopCounter++;
            DistanceFromTarget = Vector3.Distance(transform.position, TargetTower.position);
            if (DistanceFromTarget > 3.0f)
                yield return StartCoroutine("Walk");
            else
                DoDamage();
            yield return null;
        }        
    }

    IEnumerator Walk()
    {
        int LoopCounter = 0;
        CurrentState = States.walk;
        _NavMeshAgent.destination = TargetTower.position;
        _NavMeshAgent.speed = MovementSpeed;             
        while (LoopCounter <= 1000)
        {
            LoopCounter++;
            DistanceFromTarget = Vector3.Distance(transform.position, TargetTower.position);
            if (DistanceFromTarget <= 3.0f)
                yield return StartCoroutine("Idle");
            yield return null;
        }
    }

    void DoDamage(/*Take in IDamageable type*/)
    {
        //Waiting for interface to implemented
        //Debug.Log("I'm da bes");
    }

    void StopMovement(GameObject obj)
    {
        if(obj != this.gameObject)
            return;
        StopAllCoroutines();
        StartCoroutine("Idle");
    }

    [System.Serializable]
    public class EventEnemyLarvaAttack : UnityEvent<GameObject>
    {
        
    }
}
