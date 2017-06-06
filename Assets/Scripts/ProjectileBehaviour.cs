using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ProjectileBehaviour : MonoBehaviour
{
    public ParticleSystem _ExplosionParticleSystem;
    public ParticleSystem _ProjectileParticleSystem;
    private readonly List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();
    public GameObject Owner;
    public Stats ProjectileStats;

    private void Start()
    {
        _ProjectileParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Owner == null)
            return;
        transform.position = Owner.transform.position;
    }

    public IEnumerator Travel(Vector3 position)
    {
        if (_ProjectileParticleSystem == null)
            yield return null;
        transform.LookAt(position);
        yield return new WaitForSeconds(ProjectileStats.Items["projectileflighttime"].Value);
        yield return new WaitForSeconds(ProjectileStats.Items["projectilelifetime"].Value);
        Destroy(gameObject);        
        StopCoroutine(Travel(position));
    }


    private IEnumerator OnParticleCollision(GameObject other)
    {
        var numCollisions = _ProjectileParticleSystem.GetCollisionEvents(other, CollisionEvents);
        var explosion = Instantiate(_ExplosionParticleSystem, CollisionEvents[0].intersection, Quaternion.identity);
        if (other.GetComponent<IDamageable>() == null)
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(_ProjectileParticleSystem.gameObject);
            StopAllCoroutines();
            yield return null;
        }
        other.GetComponent<IDamageable>().TakeDamage(ProjectileStats.Items["projectileattackpower"].Value);
    }
}