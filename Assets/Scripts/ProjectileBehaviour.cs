using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Stats ProjectileStats;

    void Start()
    {
        GetComponent<SphereCollider>().radius = ProjectileStats.Items["projectileradius"].Value;

    }

    public IEnumerator Travel(Vector3 position)
    {
        yield return new WaitForSeconds(ProjectileStats.Items["projectileflighttime"].Value);
        this.transform.position = position;
        yield return new WaitForSeconds(ProjectileStats.Items["projectilelifetime"].Value);
        Destroy(this.gameObject);
        StopCoroutine(Travel(position));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() == null)
        {            
            return;
        }
        other.GetComponent<IDamageable>().TakeDamage(ProjectileStats.Items["projectileattackpower"].Value);
        Destroy(this.gameObject);
    }
}
