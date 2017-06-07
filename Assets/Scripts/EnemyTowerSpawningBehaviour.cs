using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyTowerSpawningBehaviour : MonoBehaviour
{
    [Serializable]
    public class EventEnemySpawn : UnityEvent<GameObject>
    {
    }

    public int maxEnemies;
    private List<GameObject> enemiesSpawned;
    public float cooldown;
    public EnemySpawner SpawnerConfig;
    public Vector3 SpawnOffsetFromRoot;
    public GameObject TargetSpawn;
    [HideInInspector]
    public EventEnemySpawn OnEnemySpawn = new EventEnemySpawn();
    [SerializeField]
    private float timer;
    private bool spawning;
    private EnemyTowerAnimationBehaviour animationBehaviour;

    public int EnemySpawnCount
    {
        get { return enemiesSpawned.Count; }
    }

    void Awake()
    {
        animationBehaviour = GetComponent<EnemyTowerAnimationBehaviour>();
    }

    void Start()
    {
        if(!SpawnerConfig)
        {
            Debug.LogError("No spawner config set. Setting the value to a default config");
            SpawnerConfig = Instantiate(Resources.Load("DefaultEnemySpawnConfig")) as EnemySpawner;
        }
        if (maxEnemies == 0)
            maxEnemies = 15;
        enemiesSpawned = new List<GameObject>();
        SpawnerConfig = Instantiate(SpawnerConfig);
        SpawnerConfig.Initialize();
        cooldown = SpawnerConfig.SpawnDelayInSeconds;
        transform.LookAt(GameObject.FindGameObjectWithTag("PlayerTower").transform);
    }

    void Update()
    {
        if(spawning)
            return;

        if(timer >= SpawnerConfig.SpawnDelayInSeconds)
        {
            spawning = true;
            animationBehaviour.DoSpawn();
        }

        timer += Time.deltaTime;
    }
    
    /// <summary>
    /// called from animation clip
    /// </summary>
    /// <param name="value">either end or start</param>
    public void Spawn(string value)
    {
        if(value != "end") return;
        
        if(TargetSpawn != null)
            SpawnOffsetFromRoot = TargetSpawn.transform.position - transform.position;
        var newSpawn = SpawnerConfig.SpawnEnemy(transform.position + SpawnOffsetFromRoot);
        enemiesSpawned.Add(newSpawn);
        var randomSize = Random.Range(1f, 1.5f);
        newSpawn.transform.localScale *= randomSize;
        timer = 0;
        spawning = false;
        if(EnemySpawnCount >= maxEnemies)
            GetComponent<IDamageable>().TakeDamage(100);
    }


#if UNITY_EDITOR

    [Tooltip("Changes the color of the gizmo that is drawn to screen to represent the spawn position of the enemy")]
    [SerializeField]
    Color GizmoColor;

    [Tooltip("Specifies weather the gizmo will be drawn as a wire frame or filled in")]
    [SerializeField]
    bool DrawWireframe = true;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        if(DrawWireframe)
            Gizmos.DrawWireCube(transform.position + SpawnOffsetFromRoot, new Vector3(1, 1, 1));
        else
            Gizmos.DrawCube(transform.position + SpawnOffsetFromRoot, new Vector3(1, 1, 1));
    }

#endif
}