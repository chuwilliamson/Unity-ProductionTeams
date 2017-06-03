using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineBehaviour : MonoBehaviour
{
    public int DamageAmount;
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
            Destroy(gameObject);
        }
    }
}
