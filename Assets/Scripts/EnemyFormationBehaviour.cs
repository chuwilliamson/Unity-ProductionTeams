using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFormationBehaviour : MonoBehaviour
{
    public float MinDistanceBetweenUnits;
    public List<EnemyMovementBehaviour> Enemies;

    public static EventUnitStop OnUnitStop = new EventUnitStop();

    public void AddNewUnit(GameObject unit)
    {
        if(Enemies.Contains(unit.GetComponent<EnemyMovementBehaviour>()))
            return;
        Enemies.Add(unit.GetComponent<EnemyMovementBehaviour>());
    }

    void Update()
    {
        foreach (var enemy in Enemies)
        {
            foreach (var neighbor in Enemies)
            {
                var Distance = Vector3.Distance(enemy.transform.position, neighbor.transform.position);
                if (Distance <= MinDistanceBetweenUnits)
                {
                    if (Vector3.Dot(enemy.transform.position - neighbor.transform.position,
                            neighbor.transform.forward) < 0)
                    {
                        OnUnitStop.Invoke(enemy.gameObject);
                    }
                }
            }
        }
    }

    public class EventUnitStop : UnityEvent<GameObject>
    {
        
    }
}
