using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerParticleBehaviour : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public ParticleSystem recoilDust;

    public void PlayMuzzleFlash(GameObject go)
    {
        PlayParticle(muzzleFlash, go);
    }

    private void PlayParticle(ParticleSystem ps, GameObject owner)
    {
        var p = Instantiate(ps, owner.transform);
        p.transform.localPosition = Vector3.up;
        p.Play();
        var duration = p.GetComponent<ParticleSystem>().main.duration;
        Destroy(p, duration);
    }
    public void OnRecoilStart()
    {
        PlayParticle(recoilDust, gameObject);
    }

    public void OnRecoilEnd()
    {

    }
}
