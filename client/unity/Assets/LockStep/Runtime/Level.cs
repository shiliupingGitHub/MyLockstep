using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace lockstep
{
    [ExecuteInEditMode]
    public class Level : MonoBehaviour
    {
#if UNITY_EDITOR
        Material mat;
        Mesh box_mesh;

        public static int minX;
        public static int maxX;
        public static int maxY;

        // Start is called before the first frame update
        void Start()
        {
            if(null == mat)
            {
                mat = new Material(Shader.Find("Unlit/ZTestOff"));
                mat.SetColor("_Color", Color.red);
                mat.renderQueue = 8000;

                box_mesh = new Mesh();

                if(gameObject.GetComponent<MeshFilter>() == null)
                     gameObject.AddComponent<MeshFilter>().mesh = box_mesh;
                if(gameObject.GetComponent<MeshRenderer>() == null)
                    gameObject.AddComponent<MeshRenderer>().material = mat;

                Update();
            }


            
        }

        private void OnDestroy()
        {

            if(null != mat)
             Object.DestroyImmediate(mat);
            if(null != box_mesh)
                Object.DestroyImmediate(box_mesh);

        }
        private void OnApplicationPause(bool pause)
        {
           
        }
        // Update is called once per frame
        void Update()
        {

            if(null != box_mesh)
            {
                box_mesh.Clear();
                List<int> indexes = new List<int>();
                List<Vector3> vetexs = new List<Vector3>();
                Vector3 v0 = new Vector3(minX / 100.0f, maxY / 100f, 0);
                Vector3 v1 = new Vector3(maxX / 100f, maxY / 100f, 0);
                Vector3 v2 = new Vector3(maxX / 100f, 0, 0);
                Vector3 v3 = new Vector3(minX / 100f, 0, 0);
                vetexs.Add(v0);
                vetexs.Add(v1);
                vetexs.Add(v2);
                vetexs.Add(v3);

                indexes.Add(0);
                indexes.Add(1);

                indexes.Add(1);
                indexes.Add(2);

                indexes.Add(2);
                indexes.Add(3);

                indexes.Add(3);
                indexes.Add(0);
                
                box_mesh.SetVertices(vetexs);
                box_mesh.SetIndices(indexes.ToArray(), MeshTopology.Lines, 0);

        
            }



        }

        
    }
#endif
}


