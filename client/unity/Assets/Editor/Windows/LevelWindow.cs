using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace lockstep
{
    public class LevelWindow : EditorWindow
    {
        bool is_new_level = false;
        int select_level_id = 0;
        int new_level_id = 0;
        Material mat;
        Mesh box_mesh;

        GameObject level_show_go;
        [MenuItem("编辑/关卡编辑窗口")]
        static void ShowWindow()
        {
            var window = GetWindow<LevelWindow>("关卡编辑窗口");

        }
        private void Awake()
        {
            LevelManager.Instance.Refresh();
            level_show_go = new GameObject("_level");
          // level_show_go.hideFlags = HideFlags.HideInHierarchy;
            level_show_go.AddComponent<Level>();
        }

        private void OnDestroy()
        {
            GameObject.DestroyImmediate(level_show_go);
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            OnMenu();
            EditorGUILayout.LabelField("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________");
            EditorGUILayout.BeginHorizontal();
            OnLevelList();
            OnCurLevel();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            BeginWindows();

            if (is_new_level)
            {
                int width = 300;
                int height = 200;
                GUILayout.Window(0, new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height), OnNewLevelWindow, "新建关卡");
            }
            EndWindows();
        }

        void OnMenu()
        {
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("新建", GUILayout.Width(100)))
            {
                is_new_level = true;
            }
            if (GUILayout.Button("全部保存", GUILayout.Width(100)))
            {
                foreach (var e in LevelManager.Instance.mInfos)
                {
                    LevelManager.Instance.Save(e);
                }

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("保存", GUILayout.Width(100)))
            {
                if (select_level_id >= 0 && select_level_id < LevelManager.Instance.mInfos.Count)
                {
                    var info = LevelManager.Instance.mInfos[select_level_id];
                    LevelManager.Instance.Save(info);
                }
            }

            if (GUILayout.Button("刷新", GUILayout.Width(100)))
            {
                LevelManager.Instance.Refresh();
            }

            EditorGUILayout.EndHorizontal();
        }

        void OnLevelList()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.Height(600));
            List<string> szSkills = new List<string>();
            foreach (var info in LevelManager.Instance.mInfos)
            {
                string name = info.id.ToString();
                szSkills.Add(name);
            }
            select_level_id = GUILayout.SelectionGrid(select_level_id, szSkills.ToArray(), 1, GUILayout.Width(100));
            EditorGUILayout.EndVertical();
        }

        void OnCurLevel()
        {
            if (select_level_id >= 0 && select_level_id < LevelManager.Instance.mInfos.Count)
            {
                var info = LevelManager.Instance.mInfos[select_level_id];

                EditorGUILayout.BeginVertical();
                int deleteIndex = -1;


                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("左边界", GUILayout.Width(50));
                info.info.minX = EditorGUILayout.IntField(info.info.minX, GUILayout.Width(100));

            
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("右边界", GUILayout.Width(50));
                info.info.maxX = EditorGUILayout.IntField(info.info.maxX, GUILayout.Width(100));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("上边界", GUILayout.Width(50));
                info.info.maxY = EditorGUILayout.IntField(info.info.maxY, GUILayout.Width(100));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();

                bool dirty = false;
                if(Level.minX != info.info.minX)
                {
                    Level.minX = info.info.minX;
                    dirty = true;
                }

                if (Level.maxX != info.info.maxX)
                {
                    Level.maxX = info.info.maxX;
                    dirty = true;
                }

                if(Level.maxY != info.info.maxY)
                {
                    Level.maxY = info.info.maxY;
                    dirty = true;
                }

                if(null != level_show_go)
                    EditorUtility.SetDirty(level_show_go);
            }
        }


        void OnNewLevelWindow(int id)
        {
            GUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("技能id:", GUILayout.Width(50));
            new_level_id = EditorGUILayout.IntField(new_level_id);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(100);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("确定", GUILayout.Width(100)))
            {
                if (LevelManager.Instance.IsLevelExist(new_level_id))
                {
                    EditorUtility.DisplayDialog("错误", "关卡ID已存在", "好的");

                }
                else
                {
                    LevelDataWrapper wrap = new LevelDataWrapper();
                    wrap.id = new_level_id;

                    LevelManager.Instance.mInfos.Add(wrap);
                    select_level_id = LevelManager.Instance.mInfos.Count - 1;

                    is_new_level = false;
                }
            }

            if (GUILayout.Button("取消", GUILayout.Width(100)))
            {
                is_new_level = false;
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}


