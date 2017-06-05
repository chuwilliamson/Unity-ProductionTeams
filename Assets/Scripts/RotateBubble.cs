using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBubble : MonoBehaviour
{
    public float theta = 0;

    public float speed = 0.5f;
	// Update is called once per frame
	void Update ()
	{
	    theta += Time.deltaTime * speed;
	    
	    transform.Rotate(Vector3.up, theta * Time.deltaTime);
	}
}
