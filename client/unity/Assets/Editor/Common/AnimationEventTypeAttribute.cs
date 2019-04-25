using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    class AnimationEventTypeAttribute : Attribute
    {
        string _EventType;

        public string EventType
        {
            get
            {
                return _EventType;
            }
        }

        public AnimationEventTypeAttribute(string eventType)
        {
            _EventType = eventType;
        }
    }
}
