using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClipBehaviour : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()
	{
	    RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 900))
        {
            var gameObjectHit = hit.collider.gameObject;
            if (!gameObjectHit.CompareTag("Player"))
            {
                var col = gameObjectHit.GetComponent<Collider>();
                var distance = hit.distance;
                var center = col.bounds.center;
                var distancefromcenter = (center * col.bounds.extents.z).magnitude;
                var distancetomove = distance + distancefromcenter;
                distancetomove = (10f + 1f) -.3f;
                Camera.main.nearClipPlane = .3f + distancetomove;

            }
        }	
	}
}
