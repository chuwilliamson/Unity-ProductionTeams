using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log("test");
    }
}