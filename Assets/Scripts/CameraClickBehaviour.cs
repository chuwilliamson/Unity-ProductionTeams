using UnityEngine;
using UnityEngine.Events;

public class CameraClickBehaviour : MonoBehaviour
{
    public LayerMask groundMask;
    public GameObject LandMinePrefab;
    public LayerMask mineMask;

    enum TowerMode
    {
        Landmine,
        Tower,
    }

    private TowerMode currentMode;
    public GameObject TowerPrefab;

    private void Start()
    {
        groundMask = LayerMask.NameToLayer("Ground");
        mineMask = LayerMask.NameToLayer("LandMine");
    }
    public class DropModeEvent : UnityEvent<string> { }

    public DropModeEvent OnDropModeChanged = new DropModeEvent();
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(2) || Input.mouseScrollDelta.magnitude > 0)
        {
            currentMode = (currentMode == TowerMode.Landmine) ? TowerMode.Tower : TowerMode.Landmine;
            OnDropModeChanged.Invoke(currentMode.ToString());
        }
        if (Input.GetKey(KeyCode.LeftAlt)) return;
        if (!Input.GetMouseButtonDown(0)) return;


        

        var screenToWorld = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(screenToWorld, out hit))
        {
            var go = (currentMode == TowerMode.Tower)
                ? Instantiate(TowerPrefab, hit.point + Vector3.up * 15f, Quaternion.identity)
                : Instantiate(LandMinePrefab, hit.point + Vector3.up * 15f, Quaternion.identity);

            var landmineBehaviour = go.GetComponent<LandMineBehaviour>();

            if(PlayerData.Instance.Gold - landmineBehaviour.goldCost.Value <= 0)
            {
                Debug.Log("you are low on cash");
                Destroy(go);
                return;
            }
            var rb = go.GetComponent<Rigidbody>();
            var dropforce = 25f;
            dropforce = go.GetComponent<LandMineBehaviour>().dropForce.Value;
            
            rb.AddForce(Vector3.down * dropforce, ForceMode.Impulse);
            
            PlayerData.Instance.SpendGold(landmineBehaviour.goldCost.Value);
        }
    }
}