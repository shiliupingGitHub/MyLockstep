using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;
namespace lockstep
{
    public class HMoveComand : BaseComand
    {
        public bool is_left { get; set; } = true;
        public bool is_move { get; set; } = true;
        public override string GetOp() => "h_move";

    }
}
