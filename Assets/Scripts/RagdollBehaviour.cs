using UnityEngine;

public class RagdollBehaviour : MonoBehaviour
{
    Rigidbody rb;

    public float force = 300f;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddExplosionForce(force, transform.position, 5f);
    }
}