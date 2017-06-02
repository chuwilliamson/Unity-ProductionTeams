using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyLarvaAnimationBehaviour : MonoBehaviour
{
    private Animator anim;
    public Stat HealthStat;
    NavMeshAgent agent;
    private void Start()
    {
        anim = GetComponent<Animator>();
        HealthStat = Instantiate(HealthStat);
        agent = GetComponent<NavMeshAgent>();
        anim.SetFloat(HEALTH, HealthStat.Value);
    }
    //idle->move: attack dead
    private int SPEED = Animator.StringToHash("speed");
    private int HEALTH = Animator.StringToHash("health");
    private int ATTACK = Animator.StringToHash("attack");

    public float animspeed;
    private void onAttack(GameObject go)
    {
        if(go != gameObject) return;
        anim.SetTrigger(ATTACK);
        anim.SetFloat(HEALTH, HealthStat.Value);
    }

    private void Update()
    {
        animspeed = agent.velocity.magnitude;
        anim.SetFloat(SPEED, animspeed);
        
    }
}
