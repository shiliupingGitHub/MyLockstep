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
    public class LevelManager : SingleTon<LevelManager>
    {
        const string FilesDir = "Assets/Res/LevelConfig";
        public List<LevelDataWrapper> mInfos = new List<LevelDataWrapper>();
        public void Refresh()
        {
            mInfos.Clear();
            DirectoryInfo di = new DirectoryInfo(FilesDir);

            var fs = di.GetFiles();

            foreach (var f in fs)
            {
                if (f.Extension.Contains(".meta"))
                    continue;
                LevelDataWrapper info = new LevelDataWrapper();
                string name = Path.GetFileNameWithoutExtension(f.FullName);
                var szInfo = name.Split('_');
                info.id = System.Convert.ToInt32(szInfo[2]);
                string text = File.ReadAllText(f.FullName);
                info.info = (LevelData)JsonMapper.ToObject(text, typeof(LevelData));
                mInfos.Add(info);

            }
        }

        public void Save(LevelDataWrapper info)
        {
            string path = System.IO.Path.Combine(FilesDir, $"level_config_{info.id}.json");
            string content = JsonMapper.ToJson(info.info);
            File.WriteAllText(path, content);
        }

        public bool IsLevelExist(int id)
        {
            foreach (var info in mInfos)
            {
                if (info.id == id)
                    return true;
            }
            return false;
        }
    }
}
