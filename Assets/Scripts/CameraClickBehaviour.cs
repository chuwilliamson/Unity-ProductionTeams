using UnityEngine;

public class CameraClickBehaviour : MonoBehaviour
{
    public LayerMask groundMask;
    public LayerMask mineMask;
    public GameObject LandMinePrefab;
    public GameObject TowerPrefab;
    public int cost;

    private void Start()
    {
        groundMask = LayerMask.NameToLayer("Ground");
        mineMask = LayerMask.NameToLayer("LandMine");
    }

    public bool TowerMode = false;
    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(2)) TowerMode = !TowerMode;
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
            var go = (TowerMode)
                ? Instantiate(TowerPrefab, hit.point + Vector3.up * 15f, Quaternion.identity)
                : Instantiate(LandMinePrefab, hit.point + Vector3.up * 15f, Quaternion.identity);
            var rb = go.GetComponent<Rigidbody>();
            var dropforce = 25f;
            dropforce = go.GetComponent<LandMineBehaviour>().dropForce.Value;
            rb.AddForce(Vector3.down * dropforce, ForceMode.Impulse);


            PlayerData.Instance.SpendGold(cost);
        }
    }
}