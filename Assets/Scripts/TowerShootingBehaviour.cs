using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[RequireComponent(typeof(TowerTargetingBehaviour))]
public class TowerShootingBehaviour : MonoBehaviour
{
    public ProjectileBehaviour ProjectilePrefab;
    [Tooltip("Spawnpoint for projectile")]
    public GameObject TowerMuzzle;
    public Transform Target;
    public Stats TowerShootingStats;

    public EventBeginFire OnShotFiredBegin = new EventBeginFire();
    public EventShotStart OnShotFiredStart = new EventShotStart();
    public EventShotLanded OnShotFiredLanded = new EventShotLanded();
    public EventShotEnded OnShotFiredEnded = new EventShotEnded();

    void Awake()
    {
        GetComponent<TowerTargetingBehaviour>().OnTargetChanged.AddListener(ChangeTarget);
    }

    void ChangeTarget(GameObject target)
    {
        Target = target.transform;
    }

    [SerializeField]
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TowerShootingStats.Items["towerfiredelay"].Value)
        {
            timer = 0;
            StartCoroutine("Shoot");
        }
    }

    void Shoot()
    {
        if(!Target)
            return;
        OnShotFiredBegin.Invoke(this.gameObject);        
        var projectile = Instantiate(ProjectilePrefab) as ProjectileBehaviour;
        projectile.transform.position = TowerMuzzle.transform.position;
        OnShotFiredStart.Invoke(this.gameObject);        
        projectile.StartCoroutine(projectile.Travel(Target.position));
        OnShotFiredLanded.Invoke(this.gameObject);          
        OnShotFiredEnded.Invoke(this.gameObject);
        StopCoroutine("Shoot");    
    }
    

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(!Target)
            return;
        Gizmos.DrawLine(TowerMuzzle.transform.position, Target.position);        
    }
#endif
    
    public class EventBeginFire : UnityEvent<GameObject>
    { }
    public class EventShotStart : UnityEvent<GameObject>
    { }
    public class EventShotLanded : UnityEvent<GameObject>
    { }
    public class EventShotEnded : UnityEvent<GameObject>
    { }
}
