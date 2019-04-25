using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core;
namespace lockstep
{
    public class ComponentsManager : SingleTon<ComponentsManager>
    {
        public Dictionary<string, Type> mCompoentTypes = new Dictionary<string, Type>();
        public List<CompoentWrap> mInfos = new List<CompoentWrap>();
        const string FilesDir = "Assets/Res/Component";

        public void Refresh()
        {
            InitClasses();
            LoadCompoentFiles();
        }

        void InitClasses()
        {
            mCompoentTypes.Clear();
            var ts = typeof(BaseComponent).Assembly.GetTypes();

            foreach (var t in ts)
            {
                var attrs = t.GetCustomAttributes(typeof(CompoentTypeAttribute), false);
                if (attrs.Length > 0)
                {
                    CompoentTypeAttribute attr = (CompoentTypeAttribute)attrs[0];

                    mCompoentTypes[attr.CompoentType] = t;
                }
            }
        }

        public BaseComponent GetCompoentById(string type, int id)
        {
            foreach(var c in mInfos)
            {
                if (c.id == id && c.type == type)
                    return c.data;
            }
            return null;
        }
        public void SaveInfo(CompoentWrap info)
        {
            string file_path = System.IO.Path.Combine(FilesDir, $"component_{info.type}_{info.id}.json");

            string content = JsonMapper.ToJson(info.data);
            File.WriteAllText(file_path, content);
        }
        void LoadCompoentFiles()
        {
            mInfos.Clear();
            DirectoryInfo di = new DirectoryInfo(FilesDir);

            var fs = di.GetFiles();

            foreach (var f in fs)
            {
                if (f.Extension.Contains(".meta"))
                    continue;
                CompoentWrap info = new CompoentWrap();
                string name = Path.GetFileNameWithoutExtension(f.FullName);
                var szInfo = name.Split('_');
                info.type = szInfo[1];
                info.id = System.Convert.ToInt32(szInfo[2]);
                Type t = mCompoentTypes[info.type];

                string text = File.ReadAllText(f.FullName);
                info.data= (BaseComponent)JsonMapper.ToObject(text, t);
                mInfos.Add(info);

            }
        }
    }
}
