using UnityEngine;
using UnityEngine.AI;

public class EnemyLarvaAnimationBehaviour : MonoBehaviour, IDamageable
{
    private readonly int ATTACK = Animator.StringToHash("attack");

    private readonly int HEALTH = Animator.StringToHash("health");

    //idle->move: attack dead
    private readonly int SPEED = Animator.StringToHash("speed");

    private NavMeshAgent agent;
    private Animator anim;
    public float animspeed;
    public Stat HealthStat;

    public float MAXSPEED = 25f;
    private float startVelocity;

    public Transform target;

    public void TakeDamage(int amount)
    {
        HealthStat.Value = HealthStat.Value - amount;
        onAttack(gameObject);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        HealthStat = Instantiate(HealthStat);
        agent = GetComponent<NavMeshAgent>();
        anim.SetFloat(HEALTH, HealthStat.Value);
        startVelocity = agent.velocity.magnitude;
    }


    private void onAttack(GameObject go)
    {
        if (go != gameObject) return;
        anim.SetTrigger(ATTACK);
        anim.SetFloat(HEALTH, HealthStat.Value);
    }

    private void Update()
    {
        animspeed = agent.velocity.magnitude;
        anim.SetFloat(SPEED, animspeed);
    }

    private void MoveStart()
    {
        agent.velocity = agent.transform.forward * MAXSPEED;
    }

    private void MoveEnd()
    {
        agent.velocity = Vector3.ClampMagnitude(agent.velocity, startVelocity);
    }

    private void DeathEnd()
    {
        PlayerData.Instance.GainExperience(25);
        PlayerData.Instance.GainGold(13);
        Destroy(gameObject, 1f);
    }
}