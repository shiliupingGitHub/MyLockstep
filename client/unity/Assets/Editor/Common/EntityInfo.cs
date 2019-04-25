using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    public class CompoentInfo
    {
        public string name;
        public int value;

    }
    public class EntityInfo
    {
        public int type = 1;
        public int visual = 1002;
        public List<CompoentInfo> components = new List<CompoentInfo>();
    }

    public class EntityInfoWrap
    {
        public int id;
        public EntityInfo info = new EntityInfo();
    }
}
