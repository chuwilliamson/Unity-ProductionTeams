using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputEvent : MonoBehaviour
{
    public void MouseEntered(GameObject obj)
    {
        Debug.Log(obj.name + " Entered collision");
    }

    public void MouseExit(GameObject obj)
    {
        Debug.Log(obj.name + " Exited collision");
    }

    public void MouseDown(GameObject obj)
    {
        Debug.Log(obj.name + " Mouse down");
    }
}
