using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerSpawningBehaviour : MonoBehaviour
{
    public EnemySpawner SpawnerConfig;
    public Vector3 SpawnOffsetFromRoot;
    [SerializeField]
    private float timer;

    void Start()
    {
        if (!SpawnerConfig)
        {
            Debug.LogError("No spawner config set. Setting the value to a default config");
            SpawnerConfig = Instantiate(Resources.Load("DefualtEnemySpawnConfig")) as EnemySpawner;
        }
        SpawnerConfig = Instantiate(SpawnerConfig);
        SpawnerConfig.Initialize();
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

#if UNITY_EDITOR    
    [Tooltip("Changes the color of the gizmo that is drawn to screen to represent the spawn position of the enemy")]
    [SerializeField]
    private Color GizmoColor;
    [Tooltip("Specifies weather the gizmo will be drawn as a wire frame or filled in")]
    [SerializeField]
    private bool DrawWireframe = true;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        if(DrawWireframe)
            Gizmos.DrawWireCube(this.transform.position + SpawnOffsetFromRoot, new Vector3(1,1,1));
        else
            Gizmos.DrawCube(this.transform.position + SpawnOffsetFromRoot, new Vector3(1, 1, 1));
    }
#endif
}
