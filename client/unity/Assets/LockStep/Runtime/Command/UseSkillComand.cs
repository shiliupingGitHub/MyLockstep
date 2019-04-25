using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    public class UseSkillComand : BaseComand
    {
        public int skill_id { get; set; }
        public override string GetOp() => "use_skill";
    }
}
