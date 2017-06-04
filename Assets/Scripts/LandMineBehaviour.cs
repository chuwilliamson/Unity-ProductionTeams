using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineBehaviour : MonoBehaviour
{
    public int DamageAmount;
    public GameObject explosion;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            var rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(DamageAmount);
            var explosiongo = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explosiongo, 2f);
            GetComponent<AudioSource>().Play();
            Destroy(gameObject,2f);
        }
    }
    
}
