using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core;
namespace hotfix
{
    public class TypesAdapter : SingleTon<TypesAdapter>
    {
        Dictionary<string, System.Type> mHotfixTypes = new Dictionary<string, Type>();

        System.Object  OnCreateHotfixType(string type)
        {
            if(mHotfixTypes.TryGetValue(type, out var t))
            {
                var ret = ObjectPool.Instance.Fetch(t);
                
                return ret;
            }
            return null;
        }
        public void Init()
        {
            mHotfixTypes.Clear();
            var types =  HotfixManager.Instance.HotFixType;
            HotfixManager.Instance.OnCreateHotfixType = OnCreateHotfixType;
            foreach (var t in types)
            {
               var attrs =  t.GetCustomAttributes(typeof(HotfixTypeAttribute), false);

                if(attrs.Length > 0)
                {
                    foreach(var attr in attrs)
                    {
                        HotfixTypeAttribute ht = (HotfixTypeAttribute)attr;
                        mHotfixTypes[ht.Name] = t;

                    }
                }

            }
        }
    }
}
