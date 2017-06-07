using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyTargetSelectionBehaviour : MonoBehaviour
{
    public NavMeshAgent Agent;
    public GameObject TargetGameObject;
    public Vector3 TargetPosition;

    public EventEnemyTargetChanged OnTargetChanged = new EventEnemyTargetChanged();

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        SearchForTarget();
    }

    void SearchForTarget()
    {
        var validTargets = GameObject.FindGameObjectsWithTag("PlayerTower").ToList();
        if (validTargets == null || validTargets.Count == 0)
        {
            return;
        }
        var sortedValidTargets = validTargets.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));
        TargetGameObject = sortedValidTargets.FirstOrDefault();
        OnTargetChanged.Invoke(TargetGameObject);
        GetDestinationTarget();
    }

    void GetDestinationTarget()
    {
        RaycastHit objectHit;

        if (Physics.Raycast(transform.position, (TargetGameObject.transform.position - transform.position).normalized, out objectHit))
        {
            if (objectHit.collider.gameObject == TargetGameObject)
            {
                var hitPoint = objectHit.point;
                TargetPosition = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                Agent.destination = TargetPosition;
            }
        }
    }

    void Update()
    {
        if (TargetGameObject == null)
        {
            SearchForTarget();
            return;
        }
    }

    void OnDrawGizmos()
    {
        if (TargetGameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, TargetPosition);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, TargetGameObject.transform.position);
        }
    }

    [System.Serializable]
    public class EventEnemyTargetChanged : UnityEvent<GameObject>
    {

    }
}
