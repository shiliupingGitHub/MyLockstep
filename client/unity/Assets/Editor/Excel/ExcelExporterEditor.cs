using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;

public struct CellInfo
{
	public string Type;
	public string Name;
	public string Desc;
    public string UsedStr;
    public string Range;
}

public class ExcelMD5Info
{
	public Dictionary<string, string> fileMD5 = new Dictionary<string, string>();

	public string Get(string fileName)
	{
		string md5 = "";
		this.fileMD5.TryGetValue(fileName, out md5);
		return md5;
	}

	public void Add(string fileName, string md5)
	{
		this.fileMD5[fileName] = md5;
	}
}

public class ExcelExporterEditor : EditorWindow
{
	[MenuItem("编辑/导出配置")]
	private static void ShowWindow()
	{
		GetWindow(typeof(ExcelExporterEditor));
	}

	private const string ExcelPath = "../config";

	private bool isClient;

	private ExcelMD5Info md5Info;
	
	// Update is called once per frame
	private void OnGUI()
	{
		try
		{

			const string clientPath = "./Assets/Res/Config";
           
			if (GUILayout.Button("导出客户端配置"))
			{
				this.isClient = true;
				
				ExportAll(clientPath);
				
				
				Debug.Log($"导出客户端配置完成!");
			}

            if(GUILayout.Button("导出配置类(c#)"))
            {
                ExportAllClass(@"./Assets/Core/Config", "using System.Collections.Generic; \nusing UnityEngine;\nusing LitJson;\n namespace core\n{\n");
                Debug.Log($"导出类完成!");
            }
            if (GUILayout.Button("导出配置类(c++)"))
            {
                ExportClasesCPP(@"./Assets/Core/Config");
            }


        }
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}
    string[] CppNames = new string[] {
        "ani_config"
    };
    private void ExportClasesCPP(string exportDir)
    {
        foreach(var f in CppNames)
        {
            string fileName = System.IO.Path.Combine(ExcelPath, $"{f}.xlsx");

            if(File.Exists(fileName))
            {
                ExportCppFiles(fileName);
            }
        }
    }

    private void ExportCppFiles(string fileName)
    {
        XSSFWorkbook xssfWorkbook;
        using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            xssfWorkbook = new XSSFWorkbook(file);
        }
        string protoName = Path.GetFileNameWithoutExtension(fileName);
        ISheet sheet = xssfWorkbook.GetSheetAt(0);
        string idType = ExportIncludeFile(sheet, protoName);
        ExportCppFile(sheet, protoName, idType);
    }
    private void ExportCppFile(ISheet sheet, string protoName, string idType)
    {
        string cpp_file_name = $"../../lockstep/lockstep/src/config/{protoName}.cpp";
        StringBuilder cppSb = new StringBuilder();
        string firetfieldName = GetCellString(sheet, 3, 0);
        int cellCount = sheet.GetRow(0).LastCellNum;

        cppSb.AppendLine($"#include \"config/{protoName}.h\"");
        cppSb.AppendLine("#include \"controller.h\"");
        cppSb.AppendLine("#include \"rapidjson/document.h\"");
        cppSb.AppendLine("#include \"config_manager.h\"");
        cppSb.AppendLine("using namespace rapidjson;");
        cppSb.AppendLine("namespace lockstep");
        cppSb.AppendLine("{");
        cppSb.AppendLine($"\tIMPLEMENT_CONFIG({protoName});");

        cppSb.AppendLine($"\t{protoName}_data* {protoName}::Get({idType} id)");

        cppSb.AppendLine("\t{");

        cppSb.AppendLine("\t\tauto it = mData.find(id);");
        cppSb.AppendLine("\t\tif (it != mData.end())");
        cppSb.AppendLine("\t\t{");
        cppSb.AppendLine("\t\t\treturn &it->second;");
        cppSb.AppendLine("\t\t}");
        cppSb.AppendLine("\t\treturn nullptr;");
        cppSb.AppendLine("\t}");

        cppSb.AppendLine($"\tvoid {protoName}::Load()");
        cppSb.AppendLine("\t{");

        cppSb.AppendLine("\t\tmData.clear();");
        cppSb.AppendLine($"\t\tstring content =  Controller::GetInstance()->InvokeReadFile(CONFIG_FILE, 0, \"{protoName}\");");
        cppSb.AppendLine("\t\tvector<string> config_str = split(content, \"\\n\");");
        cppSb.AppendLine("\t\tfor (auto it = config_str.begin(); it != config_str.end(); it++)");
        cppSb.AppendLine("\t\t{");

        cppSb.AppendLine("\t\t\tDocument d;");
        cppSb.AppendLine("\t\t\td.Parse((*it).c_str());");
        cppSb.AppendLine($"\t\t\t{protoName}_data data;");

        for(int i = 0; i < cellCount; i++)
        {
            string usedStr = GetCellString(sheet, 2, i);
            string fieldType = GetCellString(sheet, 1, i);
            string fieldName = GetCellString(sheet, 3, i);
            if (usedStr == "S" || usedStr == "N")
            {
                continue;
            }
            if (fieldType == "" || fieldName == "")
            {
                continue;
            }
            cppSb.AppendLine($"\t\t\tif (d.HasMember(\"{fieldName}\"))");
            cppSb.AppendLine("\t\t\t{");
            switch (fieldType)
            {
                case "int":

                    {
                        cppSb.AppendLine($"\t\t\t\tdata.{fieldName} = d[\"{fieldName}\"].GetInt();");
                       
                    }
                    break;
                case "float":
                    cppSb.AppendLine($"\t\t\t\tdata.{fieldName} = d[\"{fieldName}\"].GetFloat();");
                    break;
                case "string":
                    cppSb.AppendLine($"\t\t\t\tdata.{fieldName} = d[\"{fieldName}\"].GetString();");
                    break;
                case "uint":
                    cppSb.AppendLine($"\t\t\t\tdata.{fieldName} = d[\"{fieldName}\"].GetUint();");
                    break;
            }

            cppSb.AppendLine("\t\t\t}");

        }

        cppSb.AppendLine($"\t\t\tmData[data.{firetfieldName}] = data;");
        cppSb.AppendLine("\t\t}");
        cppSb.AppendLine("\t}");

        cppSb.AppendLine("}");
        File.WriteAllText(cpp_file_name, cppSb.ToString());
    }
    private string ExportIncludeFile(ISheet sheet, string protoName)
    {
        int cellCount = sheet.GetRow(0).LastCellNum;
       
        string include_file_name = $"../../lockstep/lockstep/include/config/{protoName}.h";
        

        StringBuilder headSB = new StringBuilder();

        headSB.AppendLine($"#ifndef LOCKSTEP_{protoName.ToUpper()}_H");
        headSB.AppendLine($"#define LOCKSTEP_{protoName.ToUpper()}_H");

        headSB.AppendLine("#include <string>");
        headSB.AppendLine("#include \"config/i_config.h\"");
        headSB.AppendLine("#include<map>");
        headSB.AppendLine("#include \"lockstep.h\"");
        headSB.AppendLine("using namespace std;");
        headSB.AppendLine("namespace lockstep");
        headSB.AppendLine("{");

        headSB.AppendLine($"\tstruct {protoName}_data ");
        headSB.AppendLine("\t{");
        string idType = null;
        for (int i = 0; i < cellCount; i++)
        {
            string usedStr = GetCellString(sheet, 2, i);
            string fieldType = GetCellString(sheet, 1, i);
            string fieldName = GetCellString(sheet, 3, i);
            if (usedStr == "S" || usedStr == "N")
            {
                continue;
            }
            if (fieldType == "" || fieldName == "")
            {
                continue;
            }

            switch (fieldType)
            {
                case "int":
                case "float":
                case "string":

                    if (string.IsNullOrEmpty(idType))
                    {
                        idType = fieldType;
                    }
                    headSB.AppendLine($"\t\t{fieldType} {fieldName};");
                    break;
                case "uint":
                    if (string.IsNullOrEmpty(idType))
                    {
                        idType = "unsigned int";
                    }

                    headSB.AppendLine($"\t\tunsigned int {fieldName};");
                    break;
            }


        }
        headSB.AppendLine("\t};");

        headSB.AppendLine($"\tclass {protoName} : public IConfig");
        headSB.AppendLine("\t{");
        headSB.AppendLine("\tpublic:");
        headSB.AppendLine("\t\tvirtual void Load();");
        headSB.AppendLine($"\t\t{protoName}_data* Get({idType} id);");
        headSB.AppendLine("\tprivate:");
        headSB.AppendLine($"\t\tmap<{idType}, {protoName}_data> mData;");
        headSB.AppendLine("\t};");

        headSB.AppendLine("}");
        headSB.AppendLine("#endif");

        File.WriteAllText(include_file_name, headSB.ToString());
        return idType;
    }

	private void ExportAllClass(string exportDir, string csHead)
	{
		foreach (string filePath in Directory.GetFiles(ExcelPath))
		{
			if (Path.GetExtension(filePath) != ".xlsx")
			{
				continue;
			}
			if (Path.GetFileName(filePath).StartsWith("~"))
			{
				continue;
			}

            if(filePath.Contains("msg_id_config"))
            {
                continue;
            }

			ExportClass(filePath, exportDir, csHead);
			Debug.Log($"生成{Path.GetFileName(filePath)}类");
		}
		AssetDatabase.Refresh();
	}

	private void ExportClass(string fileName, string exportDir, string csHead)
	{
		XSSFWorkbook xssfWorkbook;
		using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			xssfWorkbook = new XSSFWorkbook(file);
		}

		string protoName = Path.GetFileNameWithoutExtension(fileName);
		
		string exportPath = Path.Combine(exportDir, $"{protoName}.cs");
		using (FileStream txt = new FileStream(exportPath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(txt))
		{
			StringBuilder sb = new StringBuilder();
			ISheet sheet = xssfWorkbook.GetSheetAt(0);
            sb.Append(csHead);
           
			sb.Append($"\tpublic class {protoName}\n");
			sb.Append("\t{\n");
			sb.Append("\t\tpublic uint id { get; set; }\n");

			int cellCount = sheet.GetRow(0).LastCellNum;

       
            for (int i = 0; i < cellCount; i++)
			{
				string fieldDesc = GetCellString(sheet, 0, i);

            
				//// s开头表示这个字段是服务端专用
				//if (fieldDesc.StartsWith("s") && this.isClient)
				//{
				//	continue;
				//}
				
				string fieldName = GetCellString(sheet, 3, i);

				if (fieldName == "id" || fieldName == "_id")
				{
					continue;
				}

                string usedStr = GetCellString(sheet, 2, i);

                if(usedStr == "S" || usedStr == "N")
                {
                    continue;
                }
                string fieldType = GetCellString(sheet, 1, i);
				if (fieldType == "" || fieldName == "")
				{
					continue;
				}

				sb.Append($"\t\tpublic {fieldType} {fieldName};\n");
			}

            string first_type = GetCellString(sheet, 1, 0);
            string firest_fieldName = GetCellString(sheet, 3, 0);

            sb.AppendLine($"\t\tstatic Dictionary<{first_type},{protoName}> _Dic; ");
            sb.AppendLine($"\t\tpublic static Dictionary<{first_type},{protoName}> Dic ");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tget");
            sb.AppendLine("\t\t\t{");

            sb.AppendLine("\t\t\t\tif(_Dic == null)");

            sb.AppendLine("\t\t\t\t{");

            sb.AppendLine($"\t\t\t\t\t_Dic = new Dictionary<{first_type},{protoName}>();");
            sb.AppendLine($"\t\t\t\t\tstring text =  ResourceManager.Instance.Load<TextAsset>(\"Config/{protoName}\", \"txt\").text;");
            sb.AppendLine($"\t\t\t\t\tstring[] config_str = text.Split('\\n');");
            sb.AppendLine($"\t\t\t\t\tforeach(var str in config_str)");
            sb.AppendLine("\t\t\t\t\t{");

            sb.AppendLine($"\t\t\t\t\t\tvar config = JsonMapper.ToObject<{protoName}>(str);");
            sb.AppendLine($"\t\t\t\t\t\tif(null != config)");
            sb.AppendLine($"\t\t\t\t\t\t\t_Dic[config.{firest_fieldName}] = config;");
            sb.AppendLine("\t\t\t\t\t}");
            sb.AppendLine("\t\t\t\t}");

            sb.AppendLine("\t\t\t\treturn _Dic;");

            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t}");
            sb.Append("\t}\n");
			sb.Append("}\n");

			sw.Write(sb.ToString());
		}
	}


	private void ExportAll(string exportDir)
	{
       

        foreach (string filePath in Directory.GetFiles(ExcelPath))
		{
			if (Path.GetExtension(filePath) != ".xlsx")
			{
				continue;
			}
			if (Path.GetFileName(filePath).StartsWith("~"))
			{
				continue;
			}

			Export(filePath, exportDir);
		}


		Debug.Log("所有表导表完成");
		AssetDatabase.Refresh();
	}

	private void Export(string fileName, string exportDir)
	{
        if(fileName.Contains("msg_id_config"))
        {
            return;
        }
		XSSFWorkbook xssfWorkbook;
		using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			xssfWorkbook = new XSSFWorkbook(file);
		}
		string protoName = Path.GetFileNameWithoutExtension(fileName);
		Debug.Log($"{protoName}导表开始");
		string exportPath = Path.Combine(exportDir, $"{protoName}.txt");
		using (FileStream txt = new FileStream(exportPath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(txt))
		{
			for (int i = 0; i < xssfWorkbook.NumberOfSheets; ++i)
			{
				ISheet sheet = xssfWorkbook.GetSheetAt(i);
				ExportSheet(sheet, sw);
			}
		}
		Debug.Log($"{protoName}导表完成");
	}

	private void ExportSheet(ISheet sheet, StreamWriter sw)
	{
		int cellCount = sheet.GetRow(0).LastCellNum;

		CellInfo[] cellInfos = new CellInfo[cellCount];

		for (int i = 0; i < cellCount; i++)
		{
			string fieldDesc = GetCellString(sheet, 0, i);
			string fieldName = GetCellString(sheet, 3, i);
			string fieldType = GetCellString(sheet, 1, i);
            string usedStr = GetCellString(sheet, 2, i);
            string range = GetCellString(sheet, 5, i);
            cellInfos[i] = new CellInfo() { Name = fieldName, Type = fieldType, Desc = fieldDesc , UsedStr = usedStr, Range = range};
		}
		
		for (int i = 6; i <= sheet.LastRowNum; ++i)
		{

            IRow row = sheet.GetRow(i);

            if(null == row)
            {
                continue;
            }

            StringBuilder sb = new StringBuilder();

            if (GetCellString(row, 0) == "")
                continue;
			sb.Append("{");
			
            int k = 0;
			for (int j = 0; j < cellCount; ++j)
			{
				string desc = cellInfos[j].Desc.ToLower();

                if(cellInfos[j].UsedStr == "S" || cellInfos[j].UsedStr == "N")
                {
                    continue;
                }

				string fieldValue = GetCellString(row, j);
                //if (fieldValue == "")
                //{

                //    throw new Exception($"sheet: {sheet.SheetName} 中有空白字段 {i},{j}");
                //}

               
				string fieldName = cellInfos[j].Name;

				string fieldType = cellInfos[j].Type;

                if(fieldType == "")
                {
                    continue;
                }

                if (k > 0)
                {
                    sb.Append(",");
                }
                sb.Append($"\"{fieldName}\":{Convert(fieldType, fieldValue)}");
                k++;
			}
			sb.Append("}");
			sw.WriteLine(sb.ToString());
		}
	}

	private static string Convert(string type, string value)
	{
		switch (type)
		{
			case "int[]":
			case "int32[]":
			case "long[]":
				return $"[{value}]";
			case "string[]":
				return $"[{value}]";
			case "int":
			case "int32":
			case "int64":
			case "long":
			case "float":
			case "double":
            case "uint":
            case "bool":
				return value;
			case "string":
				return $"\"{value}\"";
			default:
				throw new Exception($"不支持此类型: {type}");
		}
	}

	private static string GetCellString(ISheet sheet, int i, int j)
	{
		return sheet.GetRow(i)?.GetCell(j)?.ToString() ?? "";
	}

	private static string GetCellString(IRow row, int i)
	{
		return row?.GetCell(i)?.ToString() ?? "";
	}

	private static string GetCellString(ICell cell)
	{
		return cell?.ToString() ?? "";
	}
}
