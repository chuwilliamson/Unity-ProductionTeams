using UnityEngine;

public class RagdollBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public GameObject bloodPool;
    public float force = 300f;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddExplosionForce(force, transform.position, 5f);
    }

    private void OnDestroy()
    {
        var bp = Instantiate(bloodPool, transform.position, Quaternion.identity);
        Destroy(bp, 5);
    }
}