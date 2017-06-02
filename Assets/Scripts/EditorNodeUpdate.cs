using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EditorNodeUpdate : MonoBehaviour
{
    public GameObject blocker;
    List<GameObject> blockers;

    public void Clear()
    {
        if(blockers == null)
            blockers = new List<GameObject>();
        if(blockers.Count > 0)
        {
            blockers.ForEach(DestroyImmediate);
            blockers.Clear();
        }
    }

    public void UpdateNodes()
    {
        foreach (var node in FindObjectsOfType<NodeBehaviour>())
        {
            var mat = node.GetComponent<Renderer>().material;
            if (node._NodeType == NodeBehaviour.NodeType.TurretBase)
            {
                mat.SetColor(Shader.PropertyToID("color"), Color.white);
                var go = Instantiate(blocker, node.transform.position, node.transform.rotation, node.transform);
                blockers.Add(go);
            }
            else if (node._NodeType == NodeBehaviour.NodeType.Wall)
            {
                mat.SetColor(Shader.PropertyToID("color"), Color.black);
            }
            else if (node._NodeType == NodeBehaviour.NodeType.Path)
            {
                mat.SetColor(Shader.PropertyToID("color"), Color.green);
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
        base.OnInspectorGUI();
        if (GUILayout.Button("Update Nodes"))
        {
            var refrence = target as EditorNodeUpdate;
            refrence.UpdateNodes();
        }
        if(GUILayout.Button("Clear Nodes"))
        {
            var refrence = target as EditorNodeUpdate;
            refrence.Clear();

        }
    }
}
#endif

