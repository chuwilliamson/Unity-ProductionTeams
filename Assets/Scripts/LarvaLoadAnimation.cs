using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarvaLoadAnimation : MonoBehaviour
{

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update ()
	{
	    anim.SetTrigger("dead");
	}

    void DeathEnd()
    {
        anim.SetTrigger("dead");
    }
}
