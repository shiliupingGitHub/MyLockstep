

namespace core
{
    public class HotfixTypeAttribute : System.Attribute
    {
        string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
        }

        public HotfixTypeAttribute(string name)
        {
            _Name = name;
        }
    }
}
