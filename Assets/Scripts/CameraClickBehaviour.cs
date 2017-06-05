using UnityEngine;

public class CameraClickBehaviour : MonoBehaviour
{
    public LayerMask groundMask;
    public GameObject prefab;
    public int cost;

    private void Start()
    {
        groundMask = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    private void Update()
    {
        if(!Input.GetMouseButtonDown(0)) return;
        

        if(PlayerData.Instance.Gold - cost <= 0)
        {
            Debug.Log("you are low on cash");
            return;
        }
        var screenToWorld = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(screenToWorld, out hit))
        {
            var go = Instantiate(prefab, hit.point + Vector3.up * 5f, Quaternion.identity);
            var rb = go.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.down * 10000f, ForceMode.Force);
            PlayerData.Instance.SpendGold(cost);
        }
    }
}