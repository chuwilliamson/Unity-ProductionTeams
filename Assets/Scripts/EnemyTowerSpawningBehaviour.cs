using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTowerSpawningBehaviour : MonoBehaviour
{
    public EnemySpawner SpawnerConfig;
    public GameObject TargetSpawn;
    public Vector3 SpawnOffsetFromRoot;
    [SerializeField]
    private float timer;

    public EventEnemySpawn OnEnemySpawn;

    void DoSpawnAnimation()
    {
        GetComponent<EnemyTowerAnimationBehaviour>().DoSpawn();
    }
    
    public void Spawn(string value)
    {
        if(!canspawn)
            return;
        timer = 0;
        if (TargetSpawn != null)
            SpawnOffsetFromRoot = TargetSpawn.transform.position - transform.position;
        var newSpawn = SpawnerConfig.SpawnEnemy(transform.position + SpawnOffsetFromRoot);
        var randomSize = Random.Range(.5f, 1.5f);
        newSpawn.transform.localScale *= randomSize;
    }

    void Start()
    {
        if (!SpawnerConfig)
        {
            Debug.LogError("No spawner config set. Setting the value to a default config");
            SpawnerConfig = Instantiate(Resources.Load("DefaultEnemySpawnConfig")) as EnemySpawner;
        }

        SpawnerConfig = Instantiate(SpawnerConfig);
        SpawnerConfig.Initialize();
        transform.LookAt(GameObject.FindGameObjectWithTag("PlayerTower").transform);
    }

    bool canspawn;
    void Update()
    {
        timer += Time.deltaTime;
        canspawn = timer >= SpawnerConfig.SpawnDelayInSeconds;
        if (canspawn)
        {
            DoSpawnAnimation();
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
    [System.Serializable]
    public class EventEnemySpawn : UnityEvent<GameObject>
    { }
}
