using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseCollision : MonoBehaviour
{
    public MouseEnterEvent OnMouseEnterEvent;
    public MouseExitEvent OnMouseExitEvent;
    public MouseButtonDown OnMouseButtonDownEvent;

    public void OnMouseEnter()
    {
        OnMouseEnterEvent.Invoke(this.gameObject);
    }

    public void OnMouseExit()
    {
        OnMouseExitEvent.Invoke(this.gameObject);
    }

    public void OnMouseDown()
    {        
        OnMouseButtonDownEvent.Invoke(this.gameObject);
    }
}

[System.Serializable]
public class MouseEnterEvent : UnityEvent<GameObject>
{ }

[System.Serializable]
public class MouseExitEvent : UnityEvent<GameObject>
{ }

[System.Serializable]
public class MouseButtonDown : UnityEvent<GameObject>
{ }
