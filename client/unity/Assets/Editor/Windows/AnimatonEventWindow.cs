using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using LitJson;
namespace lockstep
{
    public class AnimatonEventWindow : EditorWindow
    {
       
   
        int event_select_index = 0;
        int event_type_select_index = 0;
        [MenuItem("编辑/动作事件编辑窗口")]
        static void ShowWindow()
        {
            var window = GetWindow<AnimatonEventWindow>("动作事件编辑窗口");

        }

        void Awake()
        {
            AnimationEventManager.Instance.Refresh();
        }

        private void OnDestroy()
        {
            
        }

         void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            OnMenu();
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            
            OnEventList();
            GUILayout.Space(20);
            OnCurEvent();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        
        void OnMenu()
        {
            EditorGUILayout.BeginHorizontal("box");
            if(GUILayout.Button("全部保存", GUILayout.Width(100)))
            {
                foreach(var info in AnimationEventManager.Instance.mInfos)
                {
                    AnimationEventManager.Instance.SaveInfo(info);
                }

                AssetDatabase.Refresh();
            }
            if (GUILayout.Button("刷新", GUILayout.Width(100)))
            {

                AnimationEventManager.Instance.Refresh();
                
       
            }

            if (GUILayout.Button("保存当前修改", GUILayout.Width(100)))
            {
                if (event_select_index >= 0 && event_select_index < AnimationEventManager.Instance.mInfos.Count)
                {
                    var info = AnimationEventManager.Instance.mInfos[event_select_index];
                    AnimationEventManager.Instance.SaveInfo(info);
                    AssetDatabase.Refresh();
                }
            }
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.EndHorizontal();
            List<string> szTypes = new List<string>();
            foreach(var t in AnimationEventManager.Instance.mEventTypes)
            {
                szTypes.Add(t.Key);
            }

            event_type_select_index =  EditorGUILayout.Popup(event_type_select_index, szTypes.ToArray(),GUILayout.Width(100));

            if(GUILayout.Button("添加", GUILayout.Width(100)))
            {
                string szType = szTypes[event_type_select_index];
                Type t = AnimationEventManager.Instance.mEventTypes[szType];
                int id = 0;
                foreach(var info in AnimationEventManager.Instance.mInfos)
                {
                    if(info.type == szType)
                    {
                        if (info.id > id)
                            id = info.id;
                    }
                }
                id++;

                AnimationEventInfo e_info = new AnimationEventInfo();
                e_info.type = szType;
                e_info.id = id;
                e_info.data =(BaseEvent) System.Activator.CreateInstance(t);

                AnimationEventManager.Instance.mInfos.Add(e_info);
                event_select_index = AnimationEventManager.Instance.mInfos.Count - 1;
            }
            EditorGUILayout.EndHorizontal();
        }


        void OnEventList()
        {
            List<string> event_names = new List<string>();

            foreach (var info in AnimationEventManager.Instance.mInfos)
            {
                string name = $"{info.type}_{info.id}";
                event_names.Add(name);
            }
            EditorGUILayout.BeginVertical("box", GUILayout.Width(200), GUILayout.Height(600));
            event_select_index = GUILayout.SelectionGrid(event_select_index, event_names.ToArray(), 1);
            EditorGUILayout.EndVertical();
        }
        void OnCurEvent()
        {
            if(event_select_index >= 0 && event_select_index < AnimationEventManager.Instance.mInfos.Count)
            {
                var info = AnimationEventManager.Instance.mInfos[event_select_index];
                Type data_type = AnimationEventManager.Instance.mEventTypes[info.type];
                EditorGUILayout.BeginVertical("box", GUILayout.Width(300));
                ObjectGUIUtility.Show(data_type, info.data);

                EditorGUILayout.EndVertical();
            }
        }
    }
}

