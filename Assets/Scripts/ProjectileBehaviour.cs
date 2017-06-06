using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ProjectileBehaviour : MonoBehaviour
{
    public Stats ProjectileStats;
    public ParticleSystem _ProjectileParticleSystem;
    public ParticleSystem _ExplosionParticleSystem;
    public GameObject Owner;
    private List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {        
        _ProjectileParticleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        this.transform.position = Owner.transform.position;
    }

    public IEnumerator Travel(Vector3 position)
    {
        if (_ProjectileParticleSystem == null)
        {            
            yield return null;
        }
        transform.LookAt(position);
        _ProjectileParticleSystem.Play();        
        yield return new WaitForSeconds(ProjectileStats.Items["projectileflighttime"].Value);
        _ProjectileParticleSystem.Stop();
        yield return new WaitForSeconds(ProjectileStats.Items["projectilelifetime"].Value);        
        _ExplosionParticleSystem.Stop();
        Destroy(_ExplosionParticleSystem.gameObject);                
        Destroy(this.gameObject);
        StopCoroutine(Travel(position));
    }


    IEnumerator OnParticleCollision(GameObject other)
    {
        int numCollisions = _ProjectileParticleSystem.GetCollisionEvents(other, CollisionEvents);
        _ExplosionParticleSystem = Instantiate(_ExplosionParticleSystem) as ParticleSystem;
        _ExplosionParticleSystem.transform.position = CollisionEvents[0].intersection;     
        _ExplosionParticleSystem.Play();
        if (other.GetComponent<IDamageable>() == null)
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(_ExplosionParticleSystem.gameObject);
            Destroy(_ProjectileParticleSystem.gameObject);
            StopAllCoroutines();
            yield return null;
        }
        other.GetComponent<IDamageable>().TakeDamage(ProjectileStats.Items["projectileattackpower"].Value);        
    }
}
