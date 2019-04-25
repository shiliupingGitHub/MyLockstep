using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using core;
namespace lockstep
{
    public class Entity2Window : EditorWindow
    {
        bool is_new_entity = false;
        int entity_select_id = 0;
        int component_add_select_index = 0;
        int new_entity_id = 0;
        PreviewRenderUtility utility = null;
        GameObject preview_go = null;
        Rect drawRect = new Rect(0, 0, 480, 270);
        Mesh box_mesh;
        Material mat;
        [MenuItem("编辑/实体编辑窗口")]
        static void ShowWindow()
        {
            var window = GetWindow<Entity2Window>("实体编辑窗口");
        }
        private void Awake()
        {
            EntityDataManager.Instance.Refresh();
            ComponentsManager.Instance.Refresh();
            EntityPrefabManager.Instance.Refresh();

      
           
        }
        void OnEnable()
        {
            if (null == utility)
                utility = new PreviewRenderUtility();

            mat = new Material(Shader.Find("Unlit/ZTestOff"));
            mat.SetColor("_Color", Color.red);
            mat.renderQueue = 8000;
            
            box_mesh = new Mesh();

        }

        private void OnDisable()
        {
            if (null != utility)
            {
                utility.Cleanup();
                utility = null;
            }

            if (null != preview_go)
            {
                GameObject.DestroyImmediate(preview_go);
            }

            Object.DestroyImmediate(mat);
            Object.DestroyImmediate(box_mesh);
        }
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            OnMenu();
            EditorGUILayout.LabelField("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________");
            EditorGUILayout.BeginHorizontal();
            OnEntityList();
            OnCurEntity();
             OnPreview();
            EditorGUILayout.EndHorizontal();

           
            EditorGUILayout.EndVertical();



            BeginWindows();

            if (is_new_entity)
            {
                int width = 300;
                int height = 200;
                GUILayout.Window(0, new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height), OnNewEntityWindow, "新建实体");
            }
            EndWindows();
        }

        void OnPreview()
        {
            if (entity_select_id >= 0 && entity_select_id < EntityDataManager.Instance.mInfos.Count)
            {
                var info = EntityDataManager.Instance.mInfos[entity_select_id];

                if (null != preview_go && null != utility && box_mesh != null)
                {
                    utility.BeginPreview(drawRect, GUIStyle.none);


                    utility.camera.fieldOfView = 45;
                    utility.camera.orthographic = false;
                    preview_go.transform.position = Vector3.zero;
                    preview_go.transform.rotation = Quaternion.Euler(0, 90, 0);
                    preview_go.transform.localScale = Vector3.one;

                    utility.camera.transform.position = new Vector3(0, 1, -5);
                    utility.camera.clearFlags = CameraClearFlags.SolidColor;
                    utility.camera.backgroundColor = Color.black;
                    //utility.camera.nearClipPlane = 0.01f;
                    //utility.camera.farClipPlane = 1500;
                    utility.camera.orthographic = true;
                    utility.camera.orthographicSize = 2;
                    utility.camera.allowHDR = false;
                    utility.camera.allowMSAA = false;
                    utility.camera.allowDynamicResolution = false;
                    utility.AddSingleGO(preview_go);

                    if(null != _Collider2D)
                    {
                        List<Vector3> vetexs = new List<Vector3>();
                        Vector3 v0 = new Vector3((-_Collider2D.bound.size.x / 2 + _Collider2D.bound.center.x) / 100.0f, (_Collider2D.bound.center.y + _Collider2D.bound.size.y) / 100.0f, 0);
                        Vector3 v1 = new Vector3((_Collider2D.bound.size.x / 2 + _Collider2D.bound.center.x) / 100.0f, (_Collider2D.bound.center.y + _Collider2D.bound.size.y) / 100.0f, 0);
                        Vector3 v2 = new Vector3((_Collider2D.bound.size.x / 2 + _Collider2D.bound.center.x) / 100.0f, _Collider2D.bound.center.y / 100.0f, 0);
                        Vector3 v3 = new Vector3((-_Collider2D.bound.size.x / 2 + _Collider2D.bound.center.x) / 100.0f, _Collider2D.bound.center.y / 100.0f, 0);
                        vetexs.Add(v0);
                        vetexs.Add(v1);
                        vetexs.Add(v2);
                        vetexs.Add(v3);

                        box_mesh.SetVertices(vetexs);
                        box_mesh.SetIndices(new int[] { 0, 1, 1, 2, 2, 3, 3, 0 }, MeshTopology.Lines, 0);
                        utility.DrawMesh(box_mesh, Matrix4x4.identity, mat, 0);
                    }

                    utility.Render(true, true);
                    utility.cameraFieldOfView = 45;

                    var texture = utility.EndPreview();
                    GUILayout.Box(texture, GUILayout.Width(drawRect.width), GUILayout.Height(drawRect.height));
                }
            }
     
        }
        void OnMenu()
        {
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("新建", GUILayout.Width(100)))
            {
                is_new_entity = true;
            }
            if (GUILayout.Button("全部保存", GUILayout.Width(100)))
            {
                foreach (var e in EntityDataManager.Instance.mInfos)
                {
                    EntityDataManager.Instance.Save(e);
                }

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("保存", GUILayout.Width(100)))
            {
                if (entity_select_id >= 0 && entity_select_id < EntityDataManager.Instance.mInfos.Count)
                {
                    var info = EntityDataManager.Instance.mInfos[entity_select_id];
                    EntityDataManager.Instance.Save(info);
                    AssetDatabase.Refresh();
                }
            }

            if (GUILayout.Button("刷新", GUILayout.Width(100)))
            {
                EntityDataManager.Instance.Refresh();
                ComponentsManager.Instance.Refresh();
                EntityPrefabManager.Instance.Refresh();
            }

            EditorGUILayout.EndHorizontal();
        }

        void OnEntityList()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.Height(600));
            List<string> szSkills = new List<string>();
            foreach (var info in EntityDataManager.Instance.mInfos)
            {
                string name = info.id.ToString();
                szSkills.Add(name);
            }
            entity_select_id = GUILayout.SelectionGrid(entity_select_id, szSkills.ToArray(), 1, GUILayout.Width(100));
            EditorGUILayout.EndVertical();
        }
        Collider2DComponent _Collider2D = null;
        void OnCurEntity()
        {
            _Collider2D = null;
            if (entity_select_id >= 0 && entity_select_id < EntityDataManager.Instance.mInfos.Count)
            {
                var info = EntityDataManager.Instance.mInfos[entity_select_id];
                EditorGUILayout.BeginVertical(GUILayout.Width(100));
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("类型", GUILayout.Width(100));
                info.info.type = EditorGUILayout.IntField(info.info.type, GUILayout.Width(100));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("模型ID:", GUILayout.Width(100));
                int visual_select_ID = 0;
                List<string> szVisuals = new List<string>();
                for(int i = 0; i < EntityPrefabManager.Instance.mInfos.Count; i++)
                {
                    szVisuals.Add(EntityPrefabManager.Instance.mInfos[i].ToString());
                    if (info.info.visual == EntityPrefabManager.Instance.mInfos[i])
                        visual_select_ID = i;
                }
                visual_select_ID = EditorGUILayout.Popup(visual_select_ID, szVisuals.ToArray());

                info.info.visual = EntityPrefabManager.Instance.mInfos[visual_select_ID];

                if(null == preview_go)
                {
                    var t = ResourceManager.Instance.Load<GameObject>($"Visual/visual_{info.info.visual}", "prefab");
                    if(null != t)
                    {
                        preview_go = GameObject.Instantiate<GameObject>(t);
                        preview_go.name = info.info.visual.ToString();
                    }
                  
                }
                else if(preview_go.name != info.info.visual.ToString())
                {
                    GameObject.Destroy(preview_go);
                    var t = ResourceManager.Instance.Load<GameObject>($"Visual/visual_{info.info.visual}", "prefab");

                    if(null != t)
                    {
                        preview_go = GameObject.Instantiate<GameObject>(t);
                        preview_go.name = info.info.visual.ToString();
                    }

                }
                EditorGUILayout.EndHorizontal();
               // ObjectGUIUtility.Show(typeof(Size), info.info.body, "身材");
                EditorGUILayout.BeginVertical("box", GUILayout.Width(100));
                int delete = -1;
                EditorGUILayout.LabelField("组件");

                List<string> szCompoents = new List<string>();
                for (int j = 0; j <  ComponentsManager.Instance.mInfos.Count; j++)
                {
                    var c_info = ComponentsManager.Instance.mInfos[j];
                    string szName = $"{c_info.type}({c_info.id})";
                    szCompoents.Add(szName);

                   
                }
                for (int i = 0; i < info.info.components.Count; i++)
                {
                    var c_info = info.info.components[i];
                    EditorGUILayout.BeginHorizontal("box");

                    int cur_comp_select_index = 0;
                    string sz_curName = $"{c_info.name}({c_info.value})";
                    cur_comp_select_index = szCompoents.FindIndex(0, szCompoents.Count, x =>
                    {
                        return x == sz_curName;
                    });
                    cur_comp_select_index = EditorGUILayout.Popup(cur_comp_select_index, szCompoents.ToArray());

                    c_info.name = ComponentsManager.Instance.mInfos[cur_comp_select_index].type;
                    c_info.value = ComponentsManager.Instance.mInfos[cur_comp_select_index].id;
                    if (GUILayout.Button("X", GUILayout.Width(50)))
                    {
                        delete = i;
                    }
                    EditorGUILayout.EndHorizontal();

                    if (c_info.name == "collider2d")
                        _Collider2D = (Collider2DComponent)ComponentsManager.Instance.GetCompoentById("collider2d", c_info.value);
                }

                if(delete >= 0)
                {
                    info.info.components.RemoveAt(delete);
                }

               
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
                EditorGUILayout.BeginHorizontal("box");
                component_add_select_index = EditorGUILayout.Popup(component_add_select_index, szCompoents.ToArray());
                if(GUILayout.Button("添加"))
                {
                    var add_c_info = ComponentsManager.Instance.mInfos[component_add_select_index];

                    CompoentInfo n = new CompoentInfo();

                    n.name = add_c_info.type;
                    n.value = add_c_info.id;

                    info.info.components.Add(n);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }

        void OnNewEntityWindow(int id)
        {
            GUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("实体id:", GUILayout.Width(50));
            new_entity_id = EditorGUILayout.IntField(new_entity_id);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(100);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("确定", GUILayout.Width(100)))
            {
                if (EntityDataManager.Instance.IsEntityExsit(new_entity_id))
                {
                    EditorUtility.DisplayDialog("错误", "实体ID已存在", "好的");

                }
                else
                {
                    EntityInfoWrap wrap = new EntityInfoWrap();
                    wrap.id = new_entity_id;

                    wrap.info.components.Add(new CompoentInfo() { name = "hmove", value = 1 });
                    wrap.info.components.Add(new CompoentInfo() { name = "ai", value = 1 });
                    wrap.info.components.Add(new CompoentInfo() { name = "hit", value = 1 });
                    wrap.info.components.Add(new CompoentInfo() { name = "skill", value = 1 });
                    
                    EntityDataManager.Instance.mInfos.Add(wrap);
                    entity_select_id = EntityDataManager.Instance.mInfos.Count - 1;

                    is_new_entity = false;
                }
            }

            if (GUILayout.Button("取消", GUILayout.Width(100)))
            {
                is_new_entity = false;
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

    }
    
}

