using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace lockstep
{
    public class SkillWindow : EditorWindow
    {
        bool is_new_skill = false;
        int skill_select_id = 0;
        int new_skill_id = 0;
        [MenuItem("编辑/技能编辑窗口")]
        static void ShowWindow()
        {
            var window = GetWindow<SkillWindow>("技能编辑窗口");

        }
        private void Awake()
        {
            SkillManager.Instance.Refresh();
            AnimationManager.Instance.Refresh();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            OnMenu();
            EditorGUILayout.LabelField("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________");
            EditorGUILayout.BeginHorizontal();
            OnSkillListGUI();
            OnCurSkillGUI();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            BeginWindows();

            if (is_new_skill)
            {
                int width = 300;
                int height = 200;
                GUILayout.Window(0, new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height), OnNewSkillWindow, "新建技能");
            }
            EndWindows();
        }

        void OnNewSkillWindow(int id)
        {
            GUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("技能id:", GUILayout.Width(50));
            new_skill_id = EditorGUILayout.IntField(new_skill_id);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(100);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("确定", GUILayout.Width(100)))
            {
                if (SkillManager.Instance.IsSkillExist(new_skill_id))
                {
                    EditorUtility.DisplayDialog("错误", "技能ID已存在", "好的");

                }
                else
                {
                    SkillInfoWrap wrap = new SkillInfoWrap();
                    wrap.id = new_skill_id;

                    SkillManager.Instance.mInfos.Add(wrap);
                    skill_select_id = SkillManager.Instance.mInfos.Count - 1;

                    is_new_skill = false;
                }
            }

            if (GUILayout.Button("取消", GUILayout.Width(100)))
            {
                is_new_skill = false;
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.EndVertical();

        }
        void OnSkillListGUI()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.Height(600));
            List<string> szSkills = new List<string>();
            foreach(var info in SkillManager.Instance.mInfos)
            {
                string name = info.id.ToString();
                szSkills.Add(name);
            }
            skill_select_id = GUILayout.SelectionGrid(skill_select_id, szSkills.ToArray(), 1, GUILayout.Width(100));
            EditorGUILayout.EndVertical();
        }
        void OnCurSkillGUI()
        {
            
            if(skill_select_id >=0 && skill_select_id < SkillManager.Instance.mInfos.Count)
            {
                var info = SkillManager.Instance.mInfos[skill_select_id];

                EditorGUILayout.BeginVertical();
                #region ani
                int deleteIndex = -1;
                

                EditorGUILayout.BeginHorizontal("box");
                for(int i = 0; i < info.info.ani.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    GUILayout.Label("动画Id:", GUILayout.Width(100));
                    int ani_select_index = 0;
                    List<string> szAnimations = new List<string>();
                  
                    for(int j = 0; j < AnimationManager.Instance.mInfos.Count; j++)
                    {
                        var animation_info = AnimationManager.Instance.mInfos[j];

                        if (animation_info.id == info.info.ani[i])
                            ani_select_index = j;

                        szAnimations.Add(animation_info.id.ToString());
                    }
                    ani_select_index = EditorGUILayout.Popup(ani_select_index, szAnimations.ToArray(), GUILayout.Width(100));

                    info.info.ani[i] = AnimationManager.Instance.mInfos[ani_select_index].id;

                    if(GUILayout.Button("X", GUILayout.Width(30)))
                    {
                        deleteIndex = i;
                    }
                    EditorGUILayout.EndHorizontal();
                }

                if(GUILayout.Button("添加", GUILayout.Width(50)))
                {
                    if(AnimationManager.Instance.mInfos.Count > 0)
                    {
                        info.info.ani.Add(AnimationManager.Instance.mInfos[0].id);
                    }
                }
                EditorGUILayout.EndHorizontal();

                if(deleteIndex >= 0)
                {
                    info.info.ani.RemoveAt(deleteIndex);
                }
                #endregion
                EditorGUILayout.EndVertical();
            }
          
        }
        void OnMenu()
        {
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("新建", GUILayout.Width(100)))
            {
                is_new_skill = true;
            }
            if (GUILayout.Button("全部保存", GUILayout.Width(100)))
            {
                foreach (var e in SkillManager.Instance.mInfos)
                {
                    SkillManager.Instance.Save(e);
                }

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("保存", GUILayout.Width(100)))
            {
                if (skill_select_id >= 0 && skill_select_id < SkillManager.Instance.mInfos.Count)
                {
                    var info = SkillManager.Instance.mInfos[skill_select_id];
                    SkillManager.Instance.Save(info);
                }
            }

            if (GUILayout.Button("刷新", GUILayout.Width(100)))
            {
                SkillManager.Instance.Refresh();
                AnimationManager.Instance.Refresh();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}


