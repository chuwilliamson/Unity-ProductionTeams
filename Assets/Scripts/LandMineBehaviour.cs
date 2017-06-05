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
    public GameObject landSmoke;
    public Stat dropForce;
    private void Start()
    {
        enemiesinRadius = new List<IDamageable>();
        asource = GetComponent<AudioSource>();
        if(!dropForce)
            dropForce = Resources.Load<Stat>("DropForce");
        dropForce = Instantiate(dropForce);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyLarvaAnimationBehaviour>().OnDead.AddListener(RemoveFromEnemyList);
            enemiesinRadius.Add(other.GetComponent<IDamageable>());
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            enemiesinRadius.Remove(other.GetComponent<IDamageable>());
    }

    public void RemoveFromEnemyList(IDamageable enemy)
    {
        if (enemiesinRadius.Contains(enemy))
            enemiesinRadius.Remove(enemy);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            var rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            
            asource.clip = landClip;
            asource.Play();
            SpawnSmoke(gameObject);

        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(collided)
                return;
            collided = true;
            asource.clip = explosionClip;
            asource.Play();
            enemiesinRadius.ForEach(DoDamage);
            SpawnExplosion(gameObject);
            Destroy(gameObject);
        }
    }

    public void SpawnSmoke(GameObject target)
    {
        var smoke = Instantiate(landSmoke, target.transform.position, Quaternion.identity);
        smoke.hideFlags = HideFlags.HideInHierarchy;
        Destroy(smoke, 2);
    }

    public void SpawnExplosion(GameObject target)
    {
        var explosiongo = Instantiate(explosion, target.transform.position, transform.rotation);
        explosiongo.hideFlags = HideFlags.HideInHierarchy;
        Destroy(explosiongo, 2f);

    }
    public void DoDamage(IDamageable target)
    {
        if(target == null) return;
        target.TakeDamage(DamageAmount);
    }
}
