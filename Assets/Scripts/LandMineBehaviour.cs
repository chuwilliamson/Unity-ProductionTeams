using System.Collections.Generic;
using UnityEngine;

public class LandMineBehaviour : MonoBehaviour, IDamager
{
    AudioSource asource;

    public bool collided;

    [SerializeField] Stat damage;

    [SerializeField] Stat dropForce;

    public List<IDamageable> enemiesinRadius;

    //particles
    public GameObject explosion;

    //audioclips
    public AudioClip explosionClip;

    [SerializeField] Stat gold;


    public AudioClip landClip;
    public bool landed;
    public GameObject landSmoke;
    Rigidbody rb;

    public int DropForce
    {
        get { return dropForce.Value; }
    }

    public int GoldCost
    {
        get { return gold.Value; }
    }

    public void DoDamage(IDamageable target)
    {
        if (target == null) return;
        target.TakeDamage(damage.Value);
    }

    void Start()
    {
        enemiesinRadius = new List<IDamageable>();
        asource = GetComponent<AudioSource>();
        if (!dropForce)
            dropForce = Resources.Load<Stat>("DropForce");
        dropForce = Instantiate(dropForce);
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyLarvaAnimationBehaviour>().OnDead.AddListener(RemoveFromEnemyList);
            enemiesinRadius.Add(other.GetComponent<IDamageable>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            RemoveFromEnemyList(other.GetComponent<IDamageable>());
    }

    public void RemoveFromEnemyList(IDamageable enemy)
    {
        if (enemiesinRadius.Contains(enemy))
            enemiesinRadius.Remove(enemy);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (landed) return;
            landed = true;
            rb.freezeRotation = true;
            asource.clip = landClip;
            asource.Play();
            SpawnSmoke(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collided) return;
            collided = true;
            asource.clip = explosionClip;
            asource.Play();
            enemiesinRadius.ForEach(DoDamage);
            SpawnExplosion(gameObject);
            GetComponent<SimpleDamageBehaviour>().TakeDamage(25);
        }

        if (collision.gameObject.CompareTag("Ragdoll"))
        {
            rb.isKinematic = true;
            collision.gameObject.SetActive(false);
            rb.isKinematic = false;
            rb.AddForce(Vector3.down * 15f, ForceMode.Impulse);
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
}