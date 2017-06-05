using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAnimationBehaviour : MonoBehaviour
{
    
    private Animator anim;
    public Stat health;
    public static int SPAWN_TRIGGER = Animator.StringToHash("spawn");
    public static int SPAWN_STATE = Animator.StringToHash("Spawn");
    public static int HEALTH = Animator.StringToHash("health");
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        health = Instantiate(health);
        anim.SetFloat(HEALTH, health.Value);
    }

    
    //called from spawner
    public void DoSpawn()
    {
        anim.SetTrigger(SPAWN_TRIGGER);
        
    }

    public void SpawnDone()
    {
        
    } 
}
