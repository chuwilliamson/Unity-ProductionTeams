using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyTowerSpawningBehaviour : MonoBehaviour
{
    public EnemySpawner SpawnerConfig;
    public Vector3 SpawnOffsetFromRoot;
    [SerializeField]
    private float timer;

    void Start()
    {
        if(!SpawnerConfig)
            Debug.LogError("No spawner config set");
        SpawnerConfig = Instantiate(SpawnerConfig);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpawnerConfig.SpawnDelayInSeconds)
        {
            timer = 0;
            SpawnerConfig.SpawnEnemy(this.transform.position + SpawnOffsetFromRoot);   
        }
    }
}
