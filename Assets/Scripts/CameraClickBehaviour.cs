using UnityEngine;
using UnityEngine.Events;

public class CameraClickBehaviour : MonoBehaviour
{
    public LayerMask groundMask;
    public LayerMask mineMask;
    public GameObject TowerPrefab;
    public GameObject LandMinePrefab;
    enum TowerMode
    {
        Landmine,
        Tower,
    }

    private TowerMode currentMode;
    

    private void Start()
    {
        groundMask = LayerMask.NameToLayer("Ground");
        mineMask = LayerMask.NameToLayer("LandMine");
        currentMode = TowerMode.Landmine;
        OnDropModeChanged.Invoke(currentMode.ToString());
    }

    [System.Serializable]
    public class DropModeEvent : UnityEvent<string> { }
    [System.Serializable]
    public class InputRejectionEvent : UnityEvent<string> { }
    public DropModeEvent OnDropModeChanged = new DropModeEvent();
    public InputRejectionEvent OnInputReject = new InputRejectionEvent();

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(2) || Input.mouseScrollDelta.magnitude > 0)
        {
            currentMode = (currentMode == TowerMode.Landmine) ? TowerMode.Tower : TowerMode.Landmine;
            OnDropModeChanged.Invoke(currentMode.ToString());
        }

        if (Input.GetButtonDown("Fire2")) return;
        if (!Input.GetButtonDown("Fire1")) return;

        var screenToWorld = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        var prefabToSpawn = (currentMode == TowerMode.Tower) ? TowerPrefab : LandMinePrefab;
        var purchaseCost = prefabToSpawn.GetComponent<LandMineBehaviour>().GoldCost;

        if(!Physics.Raycast(screenToWorld, out hit)) return;
        var costToPurchase = PlayerData.Instance.Gold - purchaseCost;
        if (costToPurchase <= 0)
        {
            var result = string.Format("Can not buy {0} you need {1} more gold ", currentMode, Mathf.Abs(costToPurchase));
            OnInputReject.Invoke(result);
            return;
        }
            
        var go = Instantiate(prefabToSpawn, hit.point + Vector3.up * 15f, Quaternion.identity);
        var rb = go.GetComponent<Rigidbody>();

        var dropforce = go.GetComponent<LandMineBehaviour>().DropForce;
        rb.AddForce(Vector3.down * dropforce, ForceMode.Impulse);
        PlayerData.Instance.SpendGold(purchaseCost);
    }
}