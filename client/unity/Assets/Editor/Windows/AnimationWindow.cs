using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
namespace lockstep
{
    public class AnimationWindow : EditorWindow
    {
        int s_animation_select_index = 0;
        int s_frame_select = 0;
        int s_add_event_select = 0;
        bool is_new_animation = false;
        int new_animation_id = 0;
        [MenuItem("编辑/动作编辑窗口")]
        static void ShowWindow()
        {
            var window = GetWindow<AnimationWindow>("动作编辑窗口");

        }

        private void Awake()
        {
            AnimationManager.Instance.Refresh();
            AnimationEventManager.Instance.Refresh();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            OnMenu();
            EditorGUILayout.LabelField("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________");
           EditorGUILayout.Separator();
           
            EditorGUILayout.BeginHorizontal();

            OnAnimationsGUI();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical("box");
            OnCurAni();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            BeginWindows();

            if(is_new_animation)
            {
                int width = 300;
                int height = 200;
                GUILayout.Window(0, new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height), OnNewAnimationWindow, "新建动画");
            }
            EndWindows();
        }

        void OnNewAnimationWindow(int id)
        {
            GUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("动画id:", GUILayout.Width(50));
            new_animation_id =  EditorGUILayout.IntField(new_animation_id);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(100);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("确定", GUILayout.Width(100)))
            {
                if(AnimationManager.Instance.IsHasAnimation(new_animation_id))
                {
                    EditorUtility.DisplayDialog("错误", "动画ID已存在", "好的");

                }
                else
                {
                    AnimationInfoWrap wrap = new AnimationInfoWrap();
                    wrap.info.frame_num = 30;
                    wrap.info.break_frame = 20;
                    AnimationManager.Instance.mInfos.Add(wrap);
                    s_animation_select_index = AnimationManager.Instance.mInfos.Count - 1;
                    wrap.id = new_animation_id;
                    is_new_animation = false;
                }
            }

            if (GUILayout.Button("取消", GUILayout.Width(100)))
            {
                is_new_animation = false;
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.EndVertical();

        }
        void OnMenu()
        {
            EditorGUILayout.BeginHorizontal("box");
            if(GUILayout.Button("新建", GUILayout.Width(100)))
            {
                is_new_animation = true;
            }
            if (GUILayout.Button("全部保存", GUILayout.Width(100)))
            {
                foreach (var e in AnimationManager.Instance.mInfos)
                {
                    AnimationManager.Instance.Save(e);
                }

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("保存", GUILayout.Width(100)))
            {
                if (s_animation_select_index >= 0 && s_animation_select_index < AnimationManager.Instance.mInfos.Count)
                {
                    var info = AnimationManager.Instance.mInfos[s_animation_select_index];

                    AnimationManager.Instance.Save(info);

                    AssetDatabase.Refresh();
                }
            }
            if (GUILayout.Button("刷新", GUILayout.Width(100)))
            {
                AnimationManager.Instance.Refresh();
                AnimationEventManager.Instance.Refresh();
            }

            GUILayout.BeginVertical("box");
            List<string> szEvents = new List<string>();

            foreach (var e in AnimationEventManager.Instance.mInfos)
            {
                string name = $"{e.type}({e.id})";

                szEvents.Add(name);
            }

            s_add_event_select = EditorGUILayout.Popup(s_add_event_select, szEvents.ToArray(), GUILayout.Width(100));

            if (GUILayout.Button("添加事件", GUILayout.Width(100)))
            {
                if (s_animation_select_index >= 0 && s_animation_select_index < AnimationManager.Instance.mInfos.Count)
                {
                    var info = AnimationManager.Instance.mInfos[s_animation_select_index];

                    if(s_add_event_select >= 0 && s_add_event_select < AnimationEventManager.Instance.mInfos.Count)
                    {
                        var animationEventInfo = AnimationEventManager.Instance.mInfos[s_add_event_select];

                        EventInfo e_info = new EventInfo();
                        info.info.events.Add(e_info);
                        e_info.frame = s_frame_select;
                        e_info.data.type = animationEventInfo.type;
                        e_info.data.id = animationEventInfo.id;
                    }
                }
            }
            GUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
        void OnCurEvents(AnimationInfoWrap info, int select_frame)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginHorizontal("box", GUILayout.Width(100));
            List<EventInfo> deletes = new List<EventInfo>();
            foreach(var a in info.info.events)
            {
                if(a.frame == select_frame)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    GUILayout.Label($"{a.data.type}_{a.data.id}");
                   // ObjectGUIUtility.Show(a.GetType(), a);
                   if( GUILayout.Button("X", GUILayout.Width(30)))
                    {
                        deletes.Add(a);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndHorizontal();
 
            EditorGUILayout.EndHorizontal();

            foreach(var d in deletes)
            {
                info.info.events.Remove(d);
            }
        }
        void OnAnimationsGUI()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.Height(600));

            List<string> szNames = new List<string>();
            foreach(var info in AnimationManager.Instance.mInfos)
            {
                string szName = $"{info.id}";
                szNames.Add(szName);
            }
            s_animation_select_index = GUILayout.SelectionGrid(s_animation_select_index, szNames.ToArray(), 1, GUILayout.Width(100));
            EditorGUILayout.EndVertical();
        }

        void OnCurAni()
        {
            if (s_animation_select_index >= 0 && s_animation_select_index < AnimationManager.Instance.mInfos.Count)
            {
                var info = AnimationManager.Instance.mInfos[s_animation_select_index];

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("帧数", GUILayout.Width(30));
                info.info.frame_num = EditorGUILayout.IntField(info.info.frame_num, GUILayout.Width(50));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("破帧", GUILayout.Width(30));
                info.info.break_frame = EditorGUILayout.IntField(info.info.break_frame, GUILayout.Width(50));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("动作索引", GUILayout.Width(30));
                info.info.ani_index = EditorGUILayout.IntField(info.info.ani_index, GUILayout.Width(50));
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal("box");
                Color bk = GUI.backgroundColor;
                for(int i = 0; i <= info.info.frame_num; i++)
                {
                    GUI.backgroundColor = AnimationManager.Instance.IsHasEvent(info,i) ? Color.green : bk;
                    GUI.backgroundColor = i == s_frame_select ? Color.red : GUI.backgroundColor;
                    if (GUILayout.Button(i.ToString(), GUILayout.Width(30)))
                    {
                        s_frame_select = i;
                    }
                }
                GUI.backgroundColor = bk;

                EditorGUILayout.EndHorizontal();
                OnCurEvents(info, s_frame_select);

            }
        }
    }
}
