using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerAudioBehaviour : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip shotFired;
    public AudioClip targetAcquired;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    public void PlayShotFired(GameObject go)
    {
        PlayClip(shotFired);
    }

    public GameObject target;
    public void PlayTargetAcquired(GameObject go)
    {
        if(go == null)
            return;
        
        PlayClip(targetAcquired);
    }

    private void PlayClip(AudioClip ac)
    {
        audioSource.Stop();
        audioSource.clip = ac;
        audioSource.Play();
    }
}
