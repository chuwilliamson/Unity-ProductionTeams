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
    private AudioSource asource;
    public AudioClip[] audioclips;
    public AudioClip deathAudio;
    public Stat HealthStat;
    public int GoldYield = 50;
    public int ExperienceYield = 50;
    public float MAXSPEED = 25f;
    private float startVelocity;

    public Transform target;


    public void TakeDamage(int amount)
    {
        var newhealth = HealthStat.Value - amount;
        HealthStat.Value = newhealth;
        anim.SetFloat(HEALTH, newhealth);
        if (newhealth >= 1) return;
        PlayerData.Instance.GainExperience(ExperienceYield);
        PlayerData.Instance.GainGold(GoldYield);
        PlayerData.Instance.GainKills();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        asource = GetComponent<AudioSource>();

        HealthStat = Instantiate(HealthStat);
        anim.SetFloat(HEALTH, HealthStat.Value);
        startVelocity = agent.velocity.magnitude;
    }


    private void onAttack(Object go)
    {
        if (go != gameObject) return;
        anim.SetTrigger(ATTACK);
    }

    private void Update()
    {
        animspeed = agent.velocity.magnitude;
        anim.SetFloat(SPEED, animspeed);
    }

    private void MoveStart()
    {
        agent.velocity = agent.transform.forward * MAXSPEED;

        var randomclipindex = Random.Range(0, audioclips.Length - 1);
        if (asource.isPlaying) return;
        asource.clip = audioclips[randomclipindex];
        asource.Play();
    }

    private void MoveEnd()
    {
        agent.velocity = Vector3.ClampMagnitude(agent.velocity, startVelocity);
    }

    private void DeathEnd()
    {
        asource.Stop();
        asource.clip = deathAudio;
        asource.Play();
        Destroy(gameObject, 1f);
    }
}