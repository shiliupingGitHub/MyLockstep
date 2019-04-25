
using LitJson;
namespace lockstep
{
    public abstract class BaseComand
    {
        public abstract string GetOp();
        public virtual string Serilize()
        {
            return JsonMapper.ToJson(this);
        }
    }
}
