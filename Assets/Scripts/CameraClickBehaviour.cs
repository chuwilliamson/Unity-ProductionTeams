using UnityEngine;

public class CameraClickBehaviour : MonoBehaviour
{
    public LayerMask groundMask;
    public GameObject prefab;
    public int cost;

    void Start()
    {
        groundMask = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButtonDown(0)) return;
        RaycastHit hit;
        var screenToWorld = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (PlayerData.Instance.Gold - cost > 0)
        {
            if (Physics.Raycast(screenToWorld, out hit))
            {
                var go = Instantiate(prefab, hit.point + Vector3.up * 5f, Quaternion.identity);
                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.down * 10000f, ForceMode.Force);
                PlayerData.Instance.SpendGold(cost);
            }
        }
        else
        {
            Debug.Log("you are low on cash");
        }
    }
}