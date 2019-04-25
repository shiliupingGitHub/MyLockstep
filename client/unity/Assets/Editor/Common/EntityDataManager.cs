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
    class EntityDataManager : SingleTon<EntityDataManager>
    {
        const string FilesDir = "Assets/Res/EntityConfig";
        public List<EntityInfoWrap> mInfos = new List<EntityInfoWrap>();
        public void Refresh()
        {
            mInfos.Clear();
            DirectoryInfo di = new DirectoryInfo(FilesDir);

            var fs = di.GetFiles();

            foreach (var f in fs)
            {
                if (f.Extension.Contains(".meta"))
                    continue;
                EntityInfoWrap info = new EntityInfoWrap();
                string name = Path.GetFileNameWithoutExtension(f.FullName);
                var szInfo = name.Split('_');
                info.id = System.Convert.ToInt32(szInfo[2]);
                string text = File.ReadAllText(f.FullName);
                info.info = (EntityInfo)JsonMapper.ToObject(text, typeof(EntityInfo));
                mInfos.Add(info);

            }
        }

        public bool IsEntityExsit(int id)
        {
            foreach(var info in mInfos)
            {
                if (info.id == id)
                    return true;
            }

            return false;
        }
        public void Save(EntityInfoWrap info)
        {
            string path = System.IO.Path.Combine(FilesDir, $"entity_config_{info.id}.json");
            string content = JsonMapper.ToJson(info.info);
            File.WriteAllText(path, content);
        }
    }
}
