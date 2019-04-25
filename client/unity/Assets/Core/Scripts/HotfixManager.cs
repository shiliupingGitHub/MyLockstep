using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace core
{
    public class HotfixManager : IDisposable
    {
        static HotfixManager _instance;
       
        public delegate System.Object ObjectDelegate(string name);
        List<Type> mHotfixTypes;
        public ObjectDelegate OnCreateHotfixType;
#if ILRuntime
        
        public ILRuntime.Runtime.Enviorment.AppDomain appDomain;
		private MemoryStream dllStream;
		private MemoryStream pdbStream;
#else
        Assembly mHotfixAsm;
#endif

        public static HotfixManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new HotfixManager();

                    _instance.LoadAssembly();
                }

                return _instance;
            }
        }

        public List<Type> HotFixType => mHotfixTypes;

        public System.Object CreateHotfixType(string name)
        {

            if (null != OnCreateHotfixType)
                return OnCreateHotfixType(name);
            else
                return null;
        }
        void LoadAssembly()
        {
            TextAsset dll = ResourceManager.Instance.Load<TextAsset>("Code/hotfix.dll", "bytes");
            TextAsset pdb = ResourceManager.Instance.Load<TextAsset>("Code/hotfix.pdb", "bytes");
#if ILRuntime
            this.dllStream = new MemoryStream(dll.bytes);
            this.pdbStream = new MemoryStream(pdb.bytes);
            this.appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
            ILRuntimeInit.Init(appDomain);
            this.appDomain.LoadAssembly(this.dllStream, this.pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            this.mHotfixTypes = this.appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();
            var method =  this.appDomain.GetType("hotfix.Init").GetMethod("Main", 0);
            this.appDomain.Invoke(method, null, null);
#else
          

            mHotfixAsm = Assembly.Load(dll.bytes, pdb.bytes);
            mHotfixTypes = mHotfixAsm.GetTypes().ToList();
            mHotfixAsm.GetType("hotfix.Init").GetMethod("Main").Invoke(null, null);
#endif

        }
        public void Dispose()
        {
            OnCreateHotfixType = null;
#if ILRuntime
            dllStream.Dispose();
            pdbStream.Dispose();
#endif
        }
    }
}
