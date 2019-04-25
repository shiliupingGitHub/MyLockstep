using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core;
namespace lockstep
{
    public class EntityPrefabManager : SingleTon<EntityPrefabManager>
    {
        public List<int> mInfos = new List<int>();
        const string FilesDir = "Assets/Res/Visual";
        public void Refresh()
        {
            mInfos.Clear();
            DirectoryInfo di = new DirectoryInfo(FilesDir);

            var fs = di.GetFiles();

            foreach (var f in fs)
            {
                if (f.Extension.Contains(".meta"))
                    continue;
                string name = Path.GetFileNameWithoutExtension(f.FullName);
                var szInfo = name.Split('_');
                int id = System.Convert.ToInt32(szInfo[1]);
                mInfos.Add(id);

            }
        }
    }
}
