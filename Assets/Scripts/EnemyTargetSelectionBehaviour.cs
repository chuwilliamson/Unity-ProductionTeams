using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementBehaviour))]
public class EnemyTargetSelectionBehaviour : MonoBehaviour
{
    public EnemyMovementBehaviour _MovementBehaviour;
    public GameObject TargetGameObject;
    public Vector3 TargetPosition;

    void Start()
    {
        _MovementBehaviour = GetComponent<EnemyMovementBehaviour>();
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
        _MovementBehaviour.TargetTower = TargetGameObject.transform;
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
                _MovementBehaviour._NavMeshAgent.destination =TargetPosition;
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
}
