
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace core
{
    public class ResourceManager : SingleTon<ResourceManager>
    {
        public T Load<T>(string path, string ext, bool async = false) where T:UnityEngine.Object
        {
#if UNITY_EDITOR
           string final_path = $"Assets/Res/{path}.{ext}";
          return  AssetDatabase.LoadAssetAtPath<T>(final_path);
#else
    return null;
#endif

        }
    }
}
