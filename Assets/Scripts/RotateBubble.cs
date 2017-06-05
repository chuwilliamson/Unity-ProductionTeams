using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBubble : MonoBehaviour
{
    public float theta = 0;	
	// Update is called once per frame
	void Update ()
	{
	    theta += Time.deltaTime;
	    
	    transform.Rotate(Vector3.up, theta * Time.deltaTime);
	}
}
