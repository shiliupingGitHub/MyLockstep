using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using core;
namespace lockstep
{
    public class ConfigManager : SingleTon<ConfigManager>
    {
        public void Load()
        {
            ConfigManagerLoad();
        }


#if !UNITY_IPHONE
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ConfigManagerLoad();
#else
         [DllImport ("__Internal")] 
         public static extern void ConfigManagerLoad();
#endif
    }
}
