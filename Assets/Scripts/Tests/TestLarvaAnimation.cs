
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TestLarvaAnimation : MonoBehaviour
{
    public readonly EnemyLarvaAttack OnAttack = new EnemyLarvaAttack();
    public Transform target;
    NavMeshAgent agent;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(target == null)
            target = GameObject.FindGameObjectWithTag("Base").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.F))
            OnAttack.Invoke(gameObject);



        agent.SetDestination(target.position);
    }


    public class EnemyLarvaAttack : UnityEvent<GameObject> { }
}
