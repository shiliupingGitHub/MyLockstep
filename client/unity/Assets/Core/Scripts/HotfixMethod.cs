using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class HotfixMethod 
    {

#if ILRuntime
        ILRuntime.CLR.Method.IMethod method;
        ILRuntime.Runtime.Intepreter.ILTypeInstance instance;
#else
        System.Object mO;
        System.Reflection.MethodInfo method;
#endif
        public HotfixMethod(System.Object o, string name)
        {
#if ILRuntime
            instance = (ILRuntime.Runtime.Intepreter.ILTypeInstance)o;

            method = instance.Type.GetMethod(name);
#else
            mO = o;
            var type = o.GetType();
            method = type.GetMethod(name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
#endif
        }

        public void Invoke()
        {
#if ILRuntime
            if (null != method)
            {
                HotfixManager.Instance.appDomain.Invoke(method, instance, null);

            }
#else
            method?.Invoke(mO, null);
#endif
        }

        public void Invoke<T>(T o)
        {
#if ILRuntime
            if (null != method)
            {
                HotfixManager.Instance.appDomain.Invoke(method, instance, o);

            }
#else
            method?.Invoke(mO, new object[] { o });
#endif
        }
}
}
