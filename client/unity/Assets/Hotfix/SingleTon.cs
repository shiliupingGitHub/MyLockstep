

namespace hotfix
{
    public class SingleTon<T> where T:new()
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if(null == _instance)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}
