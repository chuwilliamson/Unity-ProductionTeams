using UnityEngine;

[CreateAssetMenu(fileName ="AudioPlayOneShot", menuName = "Audio/AudioPlayOneShot" )]
public class AudioPlayOneShot : ScriptableSingleton<AudioPlayOneShot>
{
    /// <summary>
    /// create an audiosource then play it
    /// </summary>
    /// <param name="ac"></param>
    /// <param name="transform"></param>
    public static void Play(AudioClip ac, Transform transform)
    {
        var go = new GameObject("AudioOneShot", typeof(AudioSource));
        go.transform.position = transform.position;
        var asource = go.GetComponent<AudioSource>();
        asource.PlayOneShot(ac);
        Destroy(go, ac.length);
    }
}
