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
    public class AnimationManager : SingleTon<AnimationManager>
    {
        const string FilesDir = "Assets/Res/AniConfig";
        public List<AnimationInfoWrap> mInfos = new List<AnimationInfoWrap>();
        public void Refresh()
        {
            mInfos.Clear();
            DirectoryInfo di = new DirectoryInfo(FilesDir);

            var fs = di.GetFiles();

            foreach (var f in fs)
            {
                if (f.Extension.Contains(".meta"))
                    continue;
                AnimationInfoWrap info = new AnimationInfoWrap();
                string name = Path.GetFileNameWithoutExtension(f.FullName);
                var szInfo = name.Split('_');
                info.id = System.Convert.ToInt32(szInfo[2]);
                string text = File.ReadAllText(f.FullName);
                info.info = (AnimationInfo)JsonMapper.ToObject(text, typeof(AnimationInfo));
                mInfos.Add(info);

            }
        }

        public void Save(AnimationInfoWrap info)
        {
            string path = System.IO.Path.Combine(FilesDir, $"ani_config_{info.id}.json");
            string content = JsonMapper.ToJson(info.info);
            File.WriteAllText(path, content);
        }
        public bool IsHasAnimation(int id)
        {
            foreach(var info in mInfos)
            {
                if (info.id == id)
                    return true;
            }
            return false;
        }
        public bool IsHasEvent(AnimationInfoWrap wrap, int frame)
        {
            foreach(var info in wrap.info.events)
            {
                if(info.frame == frame)
                {
                    return true;
                }
            }
            return false;
        }

       
    }
}
