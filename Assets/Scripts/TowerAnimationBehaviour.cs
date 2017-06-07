using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this will be on the playertower

public class TowerAnimationBehaviour : MonoBehaviour
{
    Animator anim;
	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
	    GetComponent<TowerShootingBehaviour>().OnShotFiredStart.AddListener(ShotFired);
	}

    public void ShotFired(GameObject go)
    {
        anim.SetTrigger("shotfired");
    }
	
}
