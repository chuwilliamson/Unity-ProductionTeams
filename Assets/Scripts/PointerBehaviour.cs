using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public MouseEnterEvent OnMouseEnterEvent;
    public MouseExitEvent OnMouseExitEvent;
    public MouseButtonDown OnMouseButtonDownEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterEvent.Invoke(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitEvent.Invoke(this.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
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
