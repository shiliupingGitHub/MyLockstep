using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    public class CompoentTypeAttribute : Attribute
    {
        string _CompoentType;

        public string CompoentType
        {
            get
            {
                return _CompoentType;
            }
        }

        public CompoentTypeAttribute(string componentType)
        {
            _CompoentType = componentType;
        }
    }
}
