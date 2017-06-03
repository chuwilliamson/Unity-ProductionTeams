using UnityEngine;

public class CameraClickBehaviour : MonoBehaviour
{
    public LayerMask groundMask;
    public GameObject prefab;

    void Start()
    {
        groundMask = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        RaycastHit hit;
        var screenToWorld = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenToWorld, out hit))
        {
            var go = Instantiate(prefab, hit.point + Vector3.up * 5f, Quaternion.identity);
        }
    }
}