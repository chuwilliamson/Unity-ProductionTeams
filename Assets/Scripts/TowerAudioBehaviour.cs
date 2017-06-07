using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerAudioBehaviour : MonoBehaviour
{
    public AudioSource ac;
    public AudioClip shotFired;

    private void Start()
    {
        ac = GetComponent<AudioSource>();
        GetComponent<TowerShootingBehaviour>().OnShotFiredBegin.AddListener(PlayShotFired);
    }
    // Use this for initialization
    public void PlayShotFired(GameObject go)
    {
        ac.clip = shotFired;
        ac.Play();
    }
}
