using UnityEngine;

public class MotherBaseBehaviour : SimpleDamageBehaviour
{
    public GameObject deathParticles;

    protected override void onDied(Stat s)
    {
        var go = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(go, 2f);
        base.onDied(s);
    }

    // Use this for initialization 
}