
using System.Collections.Generic;
namespace lockstep
{
    public class AnimationInfoWrap
    {
        public int id;
        public AnimationInfo info = new AnimationInfo();
    }

    public class AnimationInfo
    {
        public int frame_num;
        public int break_frame;
        public int ani_index;
        public List<EventInfo> events = new List<EventInfo>();
    }

    public class EventInfo
    {
        public int frame;
        public EventConfig data = new EventConfig();
    }

    public class EventConfig
    {
        public string type;
        public int id;
    }

}
