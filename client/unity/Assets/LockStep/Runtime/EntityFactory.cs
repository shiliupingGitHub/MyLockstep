using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core;
namespace lockstep
{
    public class EntityFactory : SingleTon<EntityFactory>
    {
    

        public Entity Create(IntPtr ptr)
        {
            return ObjectPool.Instance.Fetch<Entity>();
             
        }

    }
}
