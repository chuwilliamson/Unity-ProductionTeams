using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAnimationBehaviour : MonoBehaviour, IDamageable
{
    public static int SPAWN_TRIGGER = Animator.StringToHash("spawn");
    public static int SPAWN_STATE = Animator.StringToHash("Spawn");
    public static int HEALTH = Animator.StringToHash("health");

    Animator anim;
    AudioSource asource;
    public List<ParticleSystem> bloodParticles;
    public int ExperienceYield;
    public int GoldYield;
    public Stat HealthStat;
    public GameObject PrefabParticleSpawn;
    public AudioClip spawnAudio;
    public Transform spawnTarget;

    public void TakeDamage(int amount)
    {
        var newhealth = HealthStat.Value - amount;
        HealthStat.Value = newhealth;
        anim.SetFloat(HEALTH, newhealth);
        if (newhealth >= 1) return;
        PlayerData.Instance.GainExperience(ExperienceYield);
        PlayerData.Instance.GainGold(GoldYield);
        PlayerData.Instance.GainKills();
        PlayerData.Instance.GainBossKills();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        if (!HealthStat)
        {
            HealthStat = Resources.Load<Stat>("Health");
            HealthStat = Instantiate(HealthStat);
        }

        anim.SetFloat(HEALTH, HealthStat.Value);
        asource = GetComponent<AudioSource>();
    }


    //called from spawner
    public void DoSpawn()
    {
        anim.SetTrigger(SPAWN_TRIGGER);
    }

    /// <summary>
    ///     called from animation clip
    ///     this will spawn a particle system at the apex of animation or play audio at the start
    /// </summary>
    /// <param name="value"></param>
    public void Spawn(string value)
    {
        if (value == "end")
        {
            var smoke = Instantiate(PrefabParticleSpawn, spawnTarget.position, Quaternion.identity);
            smoke.hideFlags = HideFlags.HideInHierarchy;
            Destroy(smoke, 2);
        }
        else if (value == "start")
        {
            asource.clip = spawnAudio;
            asource.Play();
        }
    }
}