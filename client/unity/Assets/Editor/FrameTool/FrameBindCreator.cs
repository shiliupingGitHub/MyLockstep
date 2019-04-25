using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using UnityEngine.UI;
public class FrameBindCreator 
{
     const string Dir = "Assets/Hotfix/Frame/Generate";
    [MenuItem("Assets/工具/创建UI绑定脚本")]
   static void CreateBindCs()
    {
        var gos = Selection.gameObjects;

        foreach(var go in gos)
        {
            CreateBindScript(go);
        }
    }

    static void CreateBindScript(GameObject go)
    {
        ReferenceCollector rc = go.GetComponent<ReferenceCollector>();
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using core;");
        sb.AppendLine("using UnityEngine.UI;");
        sb.AppendLine("namespace hotfix");
        sb.AppendLine("{");
        sb.AppendLine($"\t[HotfixType(\"{go.name}\")]");
        sb.AppendLine($"\tpublic partial class {go.name}");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\t ReferenceCollector mRc;");
        for (int i = 0; i < rc.data.Count; i++)
        {
            ReferenceCollectorData data = rc.data[i];

            switch (data.gameObject)
            {
                case GameObject g:
                    {
                        string szType = GetGameObjectUIType(g);
                        if(!string.IsNullOrEmpty(szType))
                        {
                            sb.AppendLine($"\t\tprotected {szType} {data.key};");
                        }
                    }
                    break;
            }

        }
        sb.AppendLine("\t\t public  void OnBind(GameObject go)");
        sb.AppendLine("\t\t{");

        sb.AppendLine("\t\t\t mRc = go.GetComponent<ReferenceCollector>();");

        for (int i = 0; i < rc.data.Count; i++)
        {
            ReferenceCollectorData data = rc.data[i];

            switch (data.gameObject)
            {
                case GameObject g:
                    {
                        string szType = GetGameObjectUIType(g);
                        if (!string.IsNullOrEmpty(szType))
                        {
                            sb.AppendLine($"\t\t\t {data.key} = mRc.Get<GameObject>(\"{data.key}\").GetComponent<{szType}>();");
                        }
                    }
                    break;
            }

        }

        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string file = System.IO.Path.Combine(Dir, $"{go.name}.cs");

        File.WriteAllText(file, sb.ToString());
    }
    static string GetGameObjectUIType(GameObject go)
    {
        if(go.GetComponent<Button>() != null)
        {
            return "Button";
        }

        if(go.GetComponent<Sprite>() != null)
        {
            return "Sprite";
        }

        if(go.GetComponent<Text>() != null)
        {
            return "Text";
        }
        return null;
    }
}
