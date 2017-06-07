
using UnityEngine;

/// <summary>
/// The light spawner class.
/// </summary>
public class LightSpawner : MonoBehaviour
{
    public GameObject prefab;

    /// <summary>
    /// The spawn light function.
    /// Spawns a light.
    /// </summary>
    public void SpawnLight()
    {
        GameObject go = Instantiate(this.prefab, this.gameObject.transform.position, Quaternion.identity);
        go.transform.SetParent(this.gameObject.transform);
        if (!go.GetComponent<CircleBehaviour>())
            go.AddComponent<CircleBehaviour>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            this.SpawnLight();
    }
}
