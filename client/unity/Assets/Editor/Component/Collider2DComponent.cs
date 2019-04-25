using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    public class CVerctor2D
    {
        public int x;
        public int y;
    }

    public class CBound
    {
        public CVerctor2D center = new CVerctor2D();
        public CVerctor2D size = new CVerctor2D();
    }


    [CompoentType("collider2d")]
    public class Collider2DComponent : BaseComponent
    {
        public CBound bound;
    }
}
