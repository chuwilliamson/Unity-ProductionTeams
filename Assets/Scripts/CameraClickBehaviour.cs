using UnityEngine;

public class CameraClickBehaviour : MonoBehaviour
{
    public LayerMask groundMask;
    public LayerMask mineMask;
    public GameObject prefab;
    public int cost;

    private void Start()
    {
        groundMask = LayerMask.NameToLayer("Ground");
        mineMask = LayerMask.NameToLayer("LandMine");
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
            var go = Instantiate(prefab, hit.point + Vector3.up * 15f, Quaternion.identity);
            var rb = go.GetComponent<Rigidbody>();
            var dropforce = go.GetComponent<LandMineBehaviour>().dropForce.Value;
            rb.AddForce(Vector3.down * dropforce, ForceMode.Impulse);
            PlayerData.Instance.SpendGold(cost);
        }
    }
}