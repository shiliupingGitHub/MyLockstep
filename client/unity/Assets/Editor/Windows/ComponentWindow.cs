using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace lockstep
{
    public class ComponentWindow : EditorWindow
    {
        int compoent_select_index = 0;
        int compoent_type_select_index = 0;
        [MenuItem("编辑/组件编辑窗口")]
        static void ShowWindow()
        {
            var window = GetWindow<ComponentWindow>("组件编辑窗口");
        }

        void Awake()
        {
            ComponentsManager.Instance.Refresh();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            OnMenu();
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();

            OnCompoentList();
            GUILayout.Space(20);
            OnCurCompoent();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        void OnMenu()
        {
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("全部保存", GUILayout.Width(100)))
            {
                foreach (var info in ComponentsManager.Instance.mInfos)
                {
                    ComponentsManager.Instance.SaveInfo(info);
                }

                AssetDatabase.Refresh();
            }
            if (GUILayout.Button("刷新", GUILayout.Width(100)))
            {

                ComponentsManager.Instance.Refresh();


            }

            if (GUILayout.Button("保存当前修改", GUILayout.Width(100)))
            {
                if (compoent_select_index >= 0 && compoent_select_index < ComponentsManager.Instance.mInfos.Count)
                {
                    var info = ComponentsManager.Instance.mInfos[compoent_select_index];
                    ComponentsManager.Instance.SaveInfo(info);
                    AssetDatabase.Refresh();
                }
            }
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.EndHorizontal();
            List<string> szTypes = new List<string>();
            foreach (var t in ComponentsManager.Instance.mCompoentTypes)
            {
                szTypes.Add(t.Key);
            }

            compoent_type_select_index = EditorGUILayout.Popup(compoent_type_select_index, szTypes.ToArray(), GUILayout.Width(100));

            if (GUILayout.Button("添加", GUILayout.Width(100)))
            {
                string szType = szTypes[compoent_type_select_index];
                Type t = ComponentsManager.Instance.mCompoentTypes[szType];
                int id = 0;
                foreach (var info in ComponentsManager.Instance.mInfos)
                {
                    if (info.type == szType)
                    {
                        if (info.id > id)
                            id = info.id;
                    }
                }
                id++;

                CompoentWrap e_info = new CompoentWrap();
                e_info.type = szType;
                e_info.id = id;
                e_info.data = (BaseComponent)System.Activator.CreateInstance(t);

                ComponentsManager.Instance.mInfos.Add(e_info);
                compoent_select_index = ComponentsManager.Instance.mInfos.Count - 1;
            }
            EditorGUILayout.EndHorizontal();
        }

        void OnCompoentList()
        {

            List<string> compoents_names = new List<string>();

            foreach (var info in ComponentsManager.Instance.mInfos)
            {
                string name = $"{info.type}_{info.id}";
                compoents_names.Add(name);
            }
            EditorGUILayout.BeginVertical("box", GUILayout.Width(200), GUILayout.Height(600));
            compoent_select_index = GUILayout.SelectionGrid(compoent_select_index, compoents_names.ToArray(), 1);
            EditorGUILayout.EndVertical();

        }

        void OnCurCompoent()
        {
            if (compoent_select_index >= 0 && compoent_select_index < ComponentsManager.Instance.mInfos.Count)
            {
                var info = ComponentsManager.Instance.mInfos[compoent_select_index];
                Type data_type = ComponentsManager.Instance.mCompoentTypes[info.type];
                EditorGUILayout.BeginVertical("box", GUILayout.Width(300));
                ObjectGUIUtility.Show(data_type, info.data);

                EditorGUILayout.EndVertical();
            }
        }
    }
}

