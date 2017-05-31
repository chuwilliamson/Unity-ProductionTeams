using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementBehaviour : MonoBehaviour
{
    private NavMeshAgent _NavMeshAgent;
    private Transform PlayerTower;
    public float MovementSpeed;
    public float Display;

    enum States
    {
        idle, walk, attack, death
    }

    [SerializeField]
    private States CurrentState;

    void Start()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
        PlayerTower = GameObject.FindGameObjectWithTag("PlayerTower").transform;
        _NavMeshAgent.destination = PlayerTower.position;
        Debug.Log(PlayerTower.position);
        _NavMeshAgent.speed = MovementSpeed;
        StartCoroutine("Idle");
    }

    void Update()
    {
        Display = Vector3.Distance(transform.position, PlayerTower.position);
    }

    IEnumerator Idle()
    {
        StopCoroutines();
        CurrentState = States.idle;
        while (Vector3.Distance(transform.position, PlayerTower.position) < 5.0f)
        {
            _NavMeshAgent.isStopped = true;
            yield return null;
        }
        StartCoroutine("Walk");
    }

    IEnumerator Walk()
    {
        StopCoroutines();
        CurrentState = States.walk;
        while (Vector3.Distance(transform.position, PlayerTower.position) > 5.0f)
        {
            _NavMeshAgent.isStopped = false;
            yield return null;
        }
        StartCoroutine("Idle");        
    }

    IEnumerator Attack()
    {
        StopCoroutines();
        CurrentState = States.attack;
        if (Vector3.Distance(transform.position, _NavMeshAgent.destination) > 3.0f)
        {
            StartCoroutine("Walk");
        }
        return null;
    }

    IEnumerator Death()
    {
        StopCoroutines();
        CurrentState = States.death;
        return null;
    }

    void StopCoroutines()
    {
        switch (CurrentState)
        {
            case States.idle:
                StopCoroutine("Walk");
                StopCoroutine("Attack");
                StopCoroutine("Death");
                break;
            case States.walk:
                StopCoroutine("Idle");
                StopCoroutine("Attack");
                StopCoroutine("Death");
                break;
            case States.attack:
                StopCoroutine("Walk");
                StopCoroutine("Idle");
                StopCoroutine("Death");
                break;
            case States.death:
                StopCoroutine("Walk");
                StopCoroutine("Attack");
                StopCoroutine("Idle");
                break;
        }
    }
}
