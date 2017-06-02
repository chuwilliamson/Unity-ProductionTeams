using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAnimationBehaviour : MonoBehaviour
{
    
    private Animator anim;
    public Stat health;
    private void Start()
    {
        anim = GetComponent<Animator>();
        health = Instantiate(health);
    }

    private static int SPAWN = Animator.StringToHash("spawn");
    private static int HEALTH = Animator.StringToHash("health");
	// Update is called once per frame
    private bool begin_spawn;
	void Update ()
	{
	    anim.SetFloat(HEALTH, health.Value);
        if(begin_spawn)
	        anim.SetTrigger(SPAWN);
        
	}

    public void SpawnDone()
    {
        
    } 
}
