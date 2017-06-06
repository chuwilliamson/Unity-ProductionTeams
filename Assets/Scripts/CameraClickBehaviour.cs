using UnityEngine;

public class CameraClickBehaviour : MonoBehaviour
{
    public int cost;
    public LayerMask groundMask;
    public GameObject LandMinePrefab;
    public LayerMask mineMask;

    public bool TowerMode;
    public GameObject TowerPrefab;

    void Start()
    {
        groundMask = LayerMask.NameToLayer("Ground");
        mineMask = LayerMask.NameToLayer("LandMine");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2)) TowerMode = !TowerMode;
        if(Input.GetKey(KeyCode.LeftAlt)) return;
        if (!Input.GetMouseButtonDown(0)) return;


        if (PlayerData.Instance.Gold - cost <= 0)
        {
            Debug.Log("you are low on cash");
            return;
        }

        var screenToWorld = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(screenToWorld, out hit))
        {
            var go = TowerMode
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