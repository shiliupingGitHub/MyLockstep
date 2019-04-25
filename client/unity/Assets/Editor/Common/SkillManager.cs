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
    public class SkillManager : SingleTon<SkillManager>
    {
        const string FilesDir = "Assets/Res/SkillConfig";
        public List<SkillInfoWrap> mInfos = new List<SkillInfoWrap>();
        public void Refresh()
        {
            mInfos.Clear();
            DirectoryInfo di = new DirectoryInfo(FilesDir);

            var fs = di.GetFiles();

            foreach (var f in fs)
            {
                if (f.Extension.Contains(".meta"))
                    continue;
                SkillInfoWrap info = new SkillInfoWrap();
                string name = Path.GetFileNameWithoutExtension(f.FullName);
                var szInfo = name.Split('_');
                info.id = System.Convert.ToInt32(szInfo[2]);
                string text = File.ReadAllText(f.FullName);
                info.info = (SkillInfo)JsonMapper.ToObject(text, typeof(SkillInfo));
                mInfos.Add(info);

            }
        }
        public bool IsSkillExist(int id)
        {
            foreach(var info in mInfos)
            {
                if (info.id == id)
                    return true;
            }
            return false;
        }
        public void Save(SkillInfoWrap info)
        {
            string path = System.IO.Path.Combine(FilesDir, $"skill_config_{info.id}.json");
            string content = JsonMapper.ToJson(info.info);
            File.WriteAllText(path, content);
        }
    }
}
