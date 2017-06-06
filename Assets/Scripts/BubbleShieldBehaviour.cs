using UnityEngine;

public class BubbleShieldBehaviour : MonoBehaviour
{
    MotherBaseBehaviour motherBaseBehaviour;

    // Use this for initialization
    void Start()
    {
        motherBaseBehaviour = GetComponentInParent<MotherBaseBehaviour>();
        motherBaseBehaviour.OnDamaged.AddListener(onDamaged);
    }

    void onDamaged(Stat s)
    {
    }
    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}