using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Tower/EnemyTower", order = 1)]
public class EnemySpawner : ScriptableObject
{
    public GameObject EnemyPrefab;
    public float SpawnDelayInSeconds;

    public void SpawnEnemy(Vector3 spawn)
    {
        GameObject enemy;
        if (EnemyPrefab == null)
        {
            enemy = Instantiate(Resources.Load("DefaultEnemy")) as GameObject;
        }
        else
            enemy = Instantiate(EnemyPrefab);

        enemy.transform.position = spawn;        
    }
}
