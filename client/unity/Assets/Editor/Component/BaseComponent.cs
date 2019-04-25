using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    public class BaseComponent
    {
    }

    public class CompoentWrap
    {
        public int id;
        public string type;
        public BaseComponent data = new BaseComponent();
    }
}
