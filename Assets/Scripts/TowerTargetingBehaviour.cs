using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TowerTargetingBehaviour : MonoBehaviour
{
    [SerializeField]
    private Stats TowerTargetingStats;

    public List<EnemyMovementBehaviour> Targets;
    private EnemyMovementBehaviour CurrentTarget;
    [SerializeField]
    private GameObject RotationJoint;    

    public EventTargetChanged OnTargetChanged = new EventTargetChanged();

    void Start()
    {
        GetComponent<SphereCollider>().radius = TowerTargetingStats.Items["towerrange"].Value;
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
        if (!CurrentTarget)
        {
            FindClosestTarget();
            return;
        }
        var tarRotation = Quaternion.LookRotation(CurrentTarget.transform.position - RotationJoint.transform.position);
        RotationJoint.transform.rotation = Quaternion.Slerp(RotationJoint.transform.rotation, tarRotation, TowerTargetingStats.Items["towerrotationspeed"].Value * Time.deltaTime);
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
        if(Targets == null || Targets.Count == 0)
            return;
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
