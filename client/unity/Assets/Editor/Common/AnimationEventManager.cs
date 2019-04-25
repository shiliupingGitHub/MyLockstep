using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LitJson;
using core;
namespace lockstep
{
    class AnimationEventManager : SingleTon<AnimationEventManager>
    {
        public Dictionary<string, Type> mEventTypes = new Dictionary<string, Type>();
        public List<AnimationEventInfo> mInfos = new List<AnimationEventInfo>();
        const string FilesDir = "Assets/Res/AniEventConfig";

        public void Refresh()
        {
            InitClasses();
            LoadEventFiles();
        }
        public void SaveInfo(AnimationEventInfo info)
        {
            string file_path = System.IO.Path.Combine(FilesDir, $"event_{info.type}_{info.id}.json");

            string content = info.data.Serilize();
            File.WriteAllText(file_path, content);
        }
        void InitClasses()
        {
            mEventTypes.Clear();
            var ts = typeof(BaseEvent).Assembly.GetTypes();

            foreach (var t in ts)
            {
                var attrs = t.GetCustomAttributes(typeof(AnimationEventTypeAttribute), false);
                if (attrs.Length > 0)
                {
                    AnimationEventTypeAttribute attr = (AnimationEventTypeAttribute)attrs[0];

                    mEventTypes[attr.EventType] = t;
                }
            }
        }
        void LoadEventFiles()
        {
            mInfos.Clear();
            DirectoryInfo di = new DirectoryInfo(FilesDir);

            var fs = di.GetFiles();

            foreach (var f in fs)
            {
                if (f.Extension.Contains(".meta"))
                    continue;
                AnimationEventInfo info = new AnimationEventInfo();
                string name = Path.GetFileNameWithoutExtension(f.FullName);
                var szInfo = name.Split('_');
                info.type = szInfo[1];
                info.id = System.Convert.ToInt32(szInfo[2]);
                Type t = mEventTypes[info.type];

                string text = File.ReadAllText(f.FullName);
                info.data = (BaseEvent)JsonMapper.ToObject(text, t);
                mInfos.Add(info);

            }
        }
    }
}
