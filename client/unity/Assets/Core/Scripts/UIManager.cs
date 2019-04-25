using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace core
{
    public class UIManager :SingleTon<UIManager>
    {
        Dictionary<string, BaseFrame> mFrames = new Dictionary<string, BaseFrame>();

        public void Show(string name)
        {
            BaseFrame frame;
            if(!mFrames.TryGetValue(name,out frame))
            {
                frame = ObjectPool.Instance.Fetch<BaseFrame>();
                if (null != frame)
                    frame.Init(name);
            }

            if(null != frame)
                frame.Show();
        }
    }


}
