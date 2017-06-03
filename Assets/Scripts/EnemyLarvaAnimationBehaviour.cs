using UnityEngine;
using UnityEngine.AI;

public class EnemyLarvaAnimationBehaviour : MonoBehaviour
{
    readonly int ATTACK = Animator.StringToHash("attack");

    readonly int HEALTH = Animator.StringToHash("health");

    //idle->move: attack dead
    readonly int SPEED = Animator.StringToHash("speed");

    NavMeshAgent agent;
    Animator anim;

    public float MAXSPEED = 25f;

    public Transform target;
    public float animspeed;
    public Stat HealthStat;
    float startVelocity;
    void Start()
    {
        anim = GetComponent<Animator>();
        HealthStat = Instantiate(HealthStat);
        agent = GetComponent<NavMeshAgent>();
        anim.SetFloat(HEALTH, HealthStat.Value);
        startVelocity = agent.velocity.magnitude;
    }


    void onAttack(GameObject go)
    {
        if (go != gameObject) return;
        anim.SetTrigger(ATTACK);
        anim.SetFloat(HEALTH, HealthStat.Value);
    }

    void Update()
    {
        animspeed = agent.velocity.magnitude;
        anim.SetFloat(SPEED, animspeed);
    }

    void MoveStart()
    {
        agent.velocity = agent.transform.forward * MAXSPEED;
    }

    void MoveEnd()
    {
        agent.velocity = Vector3.ClampMagnitude(agent.velocity, startVelocity);
    }
}