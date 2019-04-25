using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    public class SkillInfo
    {
        public List<int> ani = new List<int>();
    }

    public class SkillInfoWrap
    {
        public int id;
        public SkillInfo info = new SkillInfo();
    }

}
