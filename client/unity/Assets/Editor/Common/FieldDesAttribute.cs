using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    class FieldDesAttribute : Attribute
    {
        string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        public FieldDesAttribute(string name)
        {
            _Name = name;
        }
    }
}
