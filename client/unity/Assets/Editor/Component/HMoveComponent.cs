using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    [CompoentType("hmove")]
    public class HMoveComponent : BaseComponent
    {
        public int speed = 10;
        public int x_move_reduce = -1;
    }
}
