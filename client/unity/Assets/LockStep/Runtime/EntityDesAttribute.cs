using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    public enum EntityType
    {
        NONE,
        CHARACTOR,//角色
    }
   public class EntityDesAttribute : Attribute
    {
        public EntityType Type
        {
            get
            {
                return _Type;
            }
        }
        EntityType _Type;
        public EntityDesAttribute(EntityType type)
        {
            _Type = type;
        }
    }
}
