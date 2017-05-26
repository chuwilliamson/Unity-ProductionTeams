using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorNodeUpdate : MonoBehaviour
{
    [ContextMenu("UpdateNodes")]
    void UpdateNodes()
    {
        foreach (var node in FindObjectsOfType<NodeBehaviour>())
        {
            if (node._NodeType == NodeBehaviour.NodeType.TurrentBase)
            {
                node.GetComponent<Renderer>().material.color = Color.white;
            }
            else if (node._NodeType == NodeBehaviour.NodeType.Wall)
            {
                node.GetComponent<Renderer>().material.color = Color.black;
            }
            else if (node._NodeType == NodeBehaviour.NodeType.Path)
            {
                node.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
}

