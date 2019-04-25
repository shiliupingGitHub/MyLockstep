using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace lockstep
{
   public class ObjectGUIUtility
    {

        public static void Show(Type dataType, System.Object o ,string title = null, float space = 0)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(space);
            EditorGUILayout.BeginVertical("box");
            if(!string.IsNullOrEmpty(title))
            {
                EditorGUILayout.LabelField(title, GUILayout.Width(100));
            }
            var field_info = dataType.GetFields();

            foreach (var field in field_info)
            {
                Type t = field.FieldType;
                string Name = field.Name;

               var attrs =   field.GetCustomAttributes(typeof(FieldDesAttribute), false);

                if(attrs.Length > 0)
                {
                    Name = ((FieldDesAttribute)attrs[0]).Name;
                }
                if (t == typeof(int))
                {
                    int v = (int)field.GetValue(o);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(Name, GUILayout.Width(100));
                    int v2 = EditorGUILayout.IntField(v, GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();
                    if (v != v2)
                    {
                        field.SetValue(o, v2);
                    }
                }
                else if (t == typeof(string))
                {
                    string v = (string)field.GetValue(o);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(Name, GUILayout.Width(100));
                    string v2 = EditorGUILayout.TextField( v, GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();
                    if (v != v2)
                    {
                        field.SetValue(o, v2);
                    }
                }
                else if(t == typeof(bool))
                {
                    bool v = (bool)field.GetValue(o);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(Name, GUILayout.Width(100));
                    bool v2 = EditorGUILayout.Toggle( v);
                    EditorGUILayout.EndHorizontal();
                    if (v != v2)
                    {
                        field.SetValue(o, v2);
                    }
                }
                else
                {
                    System.Object sub_o = field.GetValue(o);

                    if(sub_o == null)
                    {
                        sub_o = System.Activator.CreateInstance(t);
                        field.SetValue(o, sub_o);
                    }
                    ObjectGUIUtility.Show(t, sub_o, Name, space + 20);
                }

              

            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}
