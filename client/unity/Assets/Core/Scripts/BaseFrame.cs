using UnityEngine;
using System;
namespace core
{
    public class BaseFrame : IDisposable
    {

        public GameObject mGo;
        string mName;
        HotfixMethod mInitMethod, mOnShowMethod, mOnHideMethod, mOnDisponseMethod, mBindMethod;
        public void Init(string name)
        {
            mName = name;
            GameObject asset = ResourceManager.Instance.Load<GameObject>($"Frame/{name}", "prefab");

            mGo = GameObject.Instantiate<GameObject>(asset);
            GameObject.DontDestroyOnLoad(mGo);
            var hotfix_object = HotfixManager.Instance.CreateHotfixType(name);
            mInitMethod = new HotfixMethod(hotfix_object, "OnInit");
            mOnShowMethod = new HotfixMethod(hotfix_object, "OnShow");
            mOnHideMethod = new HotfixMethod(hotfix_object, "OnHide");
            mOnDisponseMethod = new HotfixMethod(hotfix_object, "Dispose");
            mBindMethod = new HotfixMethod(hotfix_object, "OnBind");
            mBindMethod.Invoke<GameObject>(mGo);
            mInitMethod.Invoke();
        }

        public  void Dispose()
        {
            if(null != mGo)
            {
                GameObject.Destroy(mGo);
                mGo = null;
            }
            mOnDisponseMethod.Invoke();
            ObjectPool.Instance.Recycle(this);
        }

        public void Show()
        {
            if (null != mGo)
                mGo.SetActive(true);
            mOnShowMethod.Invoke();
        }

        public void Hide()
        {
            if (null != mGo)
                mGo.SetActive(false);
            mOnHideMethod.Invoke();
        }

    }
}
