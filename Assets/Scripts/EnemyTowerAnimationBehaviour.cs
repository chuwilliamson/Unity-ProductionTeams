using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAnimationBehaviour : MonoBehaviour
{

    private Animator anim;
    public Stat health;
    public List<ParticleSystem> bloodParticles;
    public GameObject PrefabParticleSpawn;
    public Transform spawnTarget;
    public static int SPAWN_TRIGGER = Animator.StringToHash("spawn");
    public static int SPAWN_STATE = Animator.StringToHash("Spawn");
    public static int HEALTH = Animator.StringToHash("health");
    private AudioSource asource;
    public AudioClip spawnAudio;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = Instantiate(health);
        anim.SetFloat(HEALTH, health.Value);
        asource = GetComponent<AudioSource>();
    }


    //called from spawner
    public void DoSpawn()
    {
        anim.SetTrigger(SPAWN_TRIGGER);
    }

    public void ToggleBlood()
    {
        bloodParticles.ForEach(p => p.gameObject.SetActive(false));
        bloodParticles.ForEach(p => p.gameObject.SetActive(true));
    }

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

}
