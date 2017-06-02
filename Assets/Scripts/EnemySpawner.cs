using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Tower/EnemyTower", order = 1)]
public class EnemySpawner : ScriptableObject
{
    public GameObject EnemyPrefab;
    public float SpawnDelayInSeconds;
    private bool IsInitialized;

    public void Initialize()
    {
        IsInitialized = true;
    }

    public GameObject SpawnEnemy(Vector3 spawn)
    {
        if (!IsInitialized)
        {
            Debug.LogError("Scriptable Object was not Initialized properly. Be sure to invoke the Initialize function");
            return null;
        }
        GameObject enemy;
        if (EnemyPrefab == null)
        {
            enemy = Instantiate(Resources.Load("DefaultEnemy")) as GameObject;
        }
        else
            enemy = Instantiate(EnemyPrefab);

        enemy.transform.position = spawn;
        return enemy;
    }
}
