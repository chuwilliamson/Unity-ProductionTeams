using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
namespace Grid
{
    public class GridBehaviour : MonoBehaviour
    {
        [SerializeField] private AXIS axis;

        public int dims = 5;
        public List<GameObject> gameObjects;
        public float offset = 1;
        public GameObject prefab;

        [ContextMenu("Create")]
        private void CreateGrid()
        {
            if (gameObjects.Count > 0)
                DestroyGrid();
            gameObjects = new List<GameObject>();
            for (var i = 0; i < dims; i++)
            for (var j = 0; j < dims; j++)
            {
                Vector3 pos;
                switch (axis)
                {
                    case AXIS.xz:
                        pos = new Vector3(i * offset, 0, j * offset);
                        break;
                    case AXIS.xy:
                        pos = new Vector3(i * offset, j * offset, 0);
                        break;
                    case AXIS.yz:
                        pos = new Vector3(0, i * offset, j * offset);
                        break;
                    default:
                        pos = new Vector3(i * offset, 0, j * offset);
                        break;
                }


                var go = Instantiate(prefab);
                go.transform.SetParent(transform);
                go.transform.localPosition = pos;
                go.name = string.Format("Cell <{0}, {1}>", i, j);
                gameObjects.Add(go);
            }
        }

        [ContextMenu("Destroy")]
        private void DestroyGrid()
        {
            gameObjects.ForEach(go => DestroyImmediate(go));
            gameObjects.Clear();
        }

        private enum AXIS
        {
            xy,
            xz,
            yz
        }


#if UNITY_EDITOR

        [CustomEditor(typeof(GridBehaviour))]
        public class GridInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                var tar = (GridBehaviour) target;
                if (GUILayout.Button("Create Grid"))
                    tar.CreateGrid();
                if (GUILayout.Button("Destroy"))
                    tar.DestroyGrid();
            }
        }

#endif
    }
}