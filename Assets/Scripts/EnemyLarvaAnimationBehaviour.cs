using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyLarvaAnimationBehaviour : MonoBehaviour {

    private Animator anim;
    public Stat health;
    private void Start()
    {
        anim = GetComponent<Animator>();
        health = Instantiate(health);
    }
    //idle->move: attack dead
    private static int SPAWN = Animator.StringToHash("spawn");
    private static int HEALTH = Animator.StringToHash("health");
    private static readonly int ATTACK = Animator.StringToHash("attack");

    public class EnemyLarvaAttack : UnityEvent<GameObject>
    {
    }

    EnemyLarvaAttack OnAttack = new EnemyLarvaAttack();
    private void onAttack(GameObject go)
    {
        if(go != this) return;
        anim.SetTrigger(ATTACK);
    }
    
    private void Update()
    {
        
    }
}
