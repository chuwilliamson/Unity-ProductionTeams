using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineBehaviour : MonoBehaviour, IDamager
{
    public int DamageAmount;
    public GameObject explosion;
    public bool collided;
    public List<IDamageable> enemiesinRadius;
    public AudioClip landClip;
    public AudioClip explosionClip;
    private AudioSource asource;
    private void Start()
    {
        enemiesinRadius = new List<IDamageable>();
        asource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            enemiesinRadius.Add(other.GetComponent<IDamageable>());
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            enemiesinRadius.Remove(other.GetComponent<IDamageable>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            var rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            asource.clip = landClip;
            asource.Play();

        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(collided)
                return;
            collided = true;
            asource.clip = explosionClip;
            asource.Play();
            enemiesinRadius.ForEach(DoDamage);
            var explosiongo = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explosiongo, 1f);
            Destroy(gameObject, 1f);
        }
    }

   

    public void DoDamage(IDamageable target)
    {
        if(target == null) return;
        target.TakeDamage(DamageAmount);
    }
}
