using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
#if ILRuntime
    public static class ILRuntimeInit
    {

        public static void Init(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterFunctionDelegate<System.String, System.Object>();

            appdomain.DelegateManager.RegisterDelegateConvertor<core.HotfixManager.ObjectDelegate>((act) =>
            {
                return new core.HotfixManager.ObjectDelegate((name) =>
                {
                    return ((Func<System.String, System.Object>)act)(name);
                });
            });


            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
            {
                return new UnityEngine.Events.UnityAction(() =>
                {
                    ((Action)act)();
                });
            });

        }
    }
#endif
}
