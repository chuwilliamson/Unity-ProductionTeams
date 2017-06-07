using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyTargetSelectionBehaviour : MonoBehaviour
{
    public NavMeshAgent Agent;

    public EventEnemyTargetChanged OnTargetChanged = new EventEnemyTargetChanged();
    public GameObject TargetGameObject;
    public Vector3 TargetPosition;
    public LayerMask NavLayerMask;
    
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    public void SearchForTarget()
    {
        var validTargets = GameObject.FindGameObjectsWithTag("PlayerTower").ToList();
        if (validTargets.Count < 1 )
            return;
        var sortedValidTargets = validTargets.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));
        TargetGameObject = sortedValidTargets.FirstOrDefault();
        OnTargetChanged.Invoke(TargetGameObject);
        GetDestinationTarget();
    }

    private void GetDestinationTarget()
    {
        RaycastHit objectHit;
        var direction = (TargetGameObject.transform.position - transform.position);
        if (Physics.Raycast(transform.position, direction, out objectHit, Mathf.Infinity, NavLayerMask))
        {
            if (objectHit.collider.gameObject == TargetGameObject)
            {
                var hitPoint = objectHit.point;
                TargetPosition = new Vector3(hitPoint.x, 0, hitPoint.z);
                Agent.destination = TargetPosition;
            }
        }
        else
        {
            //todo:: should come up with an alternative for if we miss the raycast, we will set the larva to another layer for now            
        }
    }

    private void Update()
    {
        SearchForTarget();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (TargetGameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, TargetPosition);
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawLine(transform.position, TargetGameObject.transform.position);
        }
    }
#endif

    [Serializable]
    public class EventEnemyTargetChanged : UnityEvent<GameObject>
    {
    }
}