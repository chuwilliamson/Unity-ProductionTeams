using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NodeBehaviour : MonoBehaviour
{
    public enum NodeType
    {
        TurrentBase, Wall, Path
    }
    public Color HighlightColor;
    public Color SelectedColor;
    public bool IsSelected;
    public NodeType _NodeType;
    [SerializeField]
    private Color DefaultColor;

    void Awake()
    {
        DefaultColor = this.GetComponent<Renderer>().material.color;
        IsSelected = false;
    }

    public void Hover(bool isHovering)
    {        
        if(_NodeType != NodeType.TurrentBase)
            return;
        if(IsSelected)
            return;
        this.GetComponent<Renderer>().material.color = (isHovering) ? HighlightColor : DefaultColor;
    }

    public void SelectNode()
    {
        if(_NodeType != NodeType.TurrentBase)
            return;
        IsSelected = !IsSelected;
        this.GetComponent<Renderer>().material.color = (IsSelected) ? SelectedColor : DefaultColor;
        if(!IsSelected)
            Hover(true);
    }
}
