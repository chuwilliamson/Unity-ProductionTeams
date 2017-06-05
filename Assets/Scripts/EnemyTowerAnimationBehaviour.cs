using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAnimationBehaviour : MonoBehaviour, IDamageable
{

    private Animator anim;
    public Stat HealthStat;
    public List<ParticleSystem> bloodParticles;
    public GameObject PrefabParticleSpawn;
    public Transform spawnTarget;
    public static int SPAWN_TRIGGER = Animator.StringToHash("spawn");
    public static int SPAWN_STATE = Animator.StringToHash("Spawn");
    public static int HEALTH = Animator.StringToHash("health");
    private AudioSource asource;
    public AudioClip spawnAudio;
    public int ExperienceYield;
    public int GoldYield;

    private void Start()
    {
        anim = GetComponent<Animator>();
        HealthStat = Instantiate(HealthStat);
        anim.SetFloat(HEALTH, HealthStat.Value);
        asource = GetComponent<AudioSource>();
    }


    //called from spawner
    public void DoSpawn()
    {
        anim.SetTrigger(SPAWN_TRIGGER);
    }

    /// <summary>
    /// called from animation clip
    /// this will spawn a particle system at the apex of animation or play audio at the start
    /// </summary>
    /// <param name="value"></param>
    public void Spawn(string value)
    {
        if(value == "end")
        {
            var go = Instantiate(PrefabParticleSpawn, spawnTarget.position, Quaternion.identity);
            Destroy(go, 2);
        }
        else if (value == "start")
        {
            asource.clip = spawnAudio;
            asource.Play();
        }
    }

    public void TakeDamage(int amount)
    {
        var newhealth = HealthStat.Value - amount;
        HealthStat.Value = newhealth;
        anim.SetFloat(HEALTH, newhealth);
        if(newhealth >= 1) return;
        PlayerData.Instance.GainExperience(ExperienceYield);
        PlayerData.Instance.GainGold(GoldYield);
        PlayerData.Instance.GainKills();
    }
}
