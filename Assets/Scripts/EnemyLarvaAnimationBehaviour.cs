using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyLarvaAnimationBehaviour : MonoBehaviour, IDamageable
{
    readonly int ATTACK = Animator.StringToHash("attack");

    readonly int HEALTH = Animator.StringToHash("health");

    //idle->move: attack dead
    readonly int SPEED = Animator.StringToHash("speed");

    NavMeshAgent agent;
    Animator anim;
    public float animspeed;
    AudioSource asource;
    public AudioClip[] audioclips;
    public AudioClip deathAudio;
    public int ExperienceYield = 50;
    public int GoldYield = 50;
    public Stat HealthStat;
    public float MAXSPEED = 25f;
    float startVelocity;
    Rigidbody rb;

    public Transform target;

    public class EnemyLarvaDead : UnityEvent<IDamageable>
    {
    }

    public bool dead;
    public EnemyLarvaDead OnDead = new EnemyLarvaDead();

    public void TakeDamage(int amount)
    {
        var newhealth = HealthStat.Value - amount;
        HealthStat.Value = newhealth;
        if(anim == null) return;
        anim.SetFloat(HEALTH, newhealth);
        if (newhealth >= 1) return;
        dead = true;
        PlayerData.Instance.GainExperience(ExperienceYield);
        PlayerData.Instance.GainGold(GoldYield);
        PlayerData.Instance.GainKills();
        OnDead.Invoke(this);
        Destroy(GetComponent<EnemyMovementBehaviour>());
        Destroy(agent);
        rb.isKinematic = false;
        animspeed = 0;
        rb.AddExplosionForce(900f, transform.position, 5f);

    }

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        asource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        HealthStat = Instantiate(HealthStat);
        anim.SetFloat(HEALTH, HealthStat.Value);
        startVelocity = agent.velocity.magnitude;
        GetComponent<EnemyMovementBehaviour>().OnEnemyLarvaAttack.AddListener(onAttack);
    }


    void onAttack(GameObject go)
    {
        // Debug.Log("attack");
        if (go != gameObject) return;
        anim.SetTrigger(ATTACK);
    }

    void Update()
    {
        if(dead)
            return;
        animspeed = agent.velocity.magnitude;
        anim.SetFloat(SPEED, animspeed);
    }

    void MoveStart()
    {
        if(agent == null) return;
        agent.velocity = agent.transform.forward * MAXSPEED;

        var randomclipindex = Random.Range(0, audioclips.Length - 1);
        if (asource.isPlaying) return;
        asource.clip = audioclips[randomclipindex];
        asource.Play();
    }

    void MoveEnd()
    {
        if(agent)
        agent.velocity = Vector3.ClampMagnitude(agent.velocity, startVelocity);
    }

    void DeathEnd()
    {
        asource.Stop();
        asource.clip = deathAudio;
        asource.Play();
        
        
    }
}