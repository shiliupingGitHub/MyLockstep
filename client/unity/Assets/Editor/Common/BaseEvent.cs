using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;
namespace lockstep
{
   public class BaseEvent
    {
        public virtual string Serilize()
        {
            return JsonMapper.ToJson(this);
        }

    }
}
