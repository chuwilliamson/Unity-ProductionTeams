using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerTargetingBehaviour : MonoBehaviour
{
    [SerializeField]
    private Stats TowerTargetingStats;

    public List<EnemyMovementBehaviour> Targets;
    private EnemyMovementBehaviour CurrentTarget;
    private Quaternion DefaultOrientation;
    [SerializeField]
    private GameObject RotationJoint;    

    public EventTargetChanged OnTargetChanged = new EventTargetChanged();
    [Range(-1.0f,0.0f)]
    public float MinVerticalRotation;
    [Range(0.0f, 1.0f)]
    public float MaxVerticalRotation;

    void Start()
    {
        GetComponent<SphereCollider>().radius = TowerTargetingStats.Items["towerrange"].Value;
        DefaultOrientation = RotationJoint.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyMovementBehaviour>())
        {
            Targets.Add(other.GetComponent<EnemyMovementBehaviour>());
        }
        FindClosestTarget();
    }

    void OnTriggerExit(Collider other)
    {
        if (Targets.Contains(other.GetComponent<EnemyMovementBehaviour>()))
        {
            Targets.Remove(other.GetComponent<EnemyMovementBehaviour>());
            CurrentTarget = null;
        }
        FindClosestTarget();
    }

    void Update()
    {        
        if (!CurrentTarget || CurrentTarget.GetComponent<EnemyLarvaAnimationBehaviour>().dead)
        {
            RotationJoint.transform.rotation = Quaternion.Slerp(RotationJoint.transform.rotation, DefaultOrientation, TowerTargetingStats.Items["towerrotationspeed"].Value * Time.deltaTime);
            FindClosestTarget();
            return;
        }
        
        var targetRotation = Quaternion.LookRotation(CurrentTarget.transform.position - RotationJoint.transform.position);
        targetRotation.x = Mathf.Clamp(targetRotation.x, MinVerticalRotation, MaxVerticalRotation);
        targetRotation.z = Mathf.Clamp(targetRotation.z, MinVerticalRotation, MaxVerticalRotation);
        RotationJoint.transform.rotation = Quaternion.Slerp(RotationJoint.transform.rotation, targetRotation, TowerTargetingStats.Items["towerrotationspeed"].Value * Time.deltaTime);        
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(CurrentTarget)
            Gizmos.DrawLine(this.transform.position, CurrentTarget.transform.position);
    }
#endif

    void FindClosestTarget()
    {
        var temp = Targets;
        for (int i = 0; i < temp.Count; i++)
        {
            if(temp[i] == null)
                temp.RemoveAt(i);
        }
        Targets = temp;
        if (Targets == null || Targets.Count == 0)
        {
            CurrentTarget = null;
            OnTargetChanged.Invoke(null);
            return;            
        }
        Targets = Targets.OrderBy(x => x.DistanceFromTarget).ToList();
        CurrentTarget = Targets[0];
        if (!CurrentTarget)
        {            
            return;
        }
        OnTargetChanged.Invoke(CurrentTarget.gameObject);
    }

    public class EventTargetChanged : UnityEvent<GameObject>
    { }
}   
