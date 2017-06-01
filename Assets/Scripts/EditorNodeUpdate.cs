using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EditorNodeUpdate : MonoBehaviour
{
    public void UpdateNodes()
    {
        foreach (var node in FindObjectsOfType<NodeBehaviour>())
        {
            if (node._NodeType == NodeBehaviour.NodeType.TurretBase)
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

#if UNITY_EDITOR
[CustomEditor(typeof(EditorNodeUpdate))]
class EditorNode : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Update Nodes"))
        {
            var refrence = target as EditorNodeUpdate;
            refrence.UpdateNodes();
        }
    }
}
#endif

