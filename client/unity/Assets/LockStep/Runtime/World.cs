using System;
using System.Runtime.InteropServices;

namespace lockstep
{
    public class World : IDisposable
    {
        IntPtr _interval;
        public World()
        {
           _interval = NewWorld();
        }

        public  void Dispose()
        {
            DeleteWorld(_interval);
            core.ObjectPool.Instance.Recycle(this);
        }

        public bool Load(LevelData data)
        {

            return WorldLoad(_interval,  data);
        }

        public bool LoadEntitys(EntityData[] datas)
        {
            return WorldLoadEntitys(_interval, datas, datas.Length);
        }

        public void TickCmd(Command[] data)
        {
            if(null != data)
            {
                WorldTickCmd(_interval, data, data.Length);
            }
        }

        public void Tick()
        {
            WorldTick(_interval);
        }


#if !UNITY_IPHONE
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NewWorld();

        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void  DeleteWorld(IntPtr world_ptr);

        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool WorldLoad(IntPtr world_ptr, LevelData data);

        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WorldTickCmd(IntPtr world_ptr, Command[] data, int num);

        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool WorldTick(IntPtr world_ptr);


        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool WorldLoadEntitys(IntPtr world_ptr, EntityData[] data, int num);

        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WorldGetController(IntPtr world_ptr);


#else
        [DllImport ("__Internal")] 
        public static extern IntPtr NewWorld();
        [DllImport ("__Internal")] 
         public static extern void  DeleteWorld(IntPtr world_ptr);
         [DllImport ("__Internal")] 
         public static extern bool WorldLoad(IntPtr world_ptr, LevelData data);
        [DllImport ("__Internal")] 
        public static extern bool WorldTickCmd(IntPtr world_ptr, Command[] data, int num);
        [DllImport ("__Internal")] 
        public static extern bool WorldTick(IntPtr world_ptr);
        [DllImport ("__Internal")] 
        public static extern bool WorldLoadEntitys(IntPtr world_ptr, EntityData[] data, int num);
        [DllImport ("__Internal")] 
        public static extern IntPtr WorldGetController(IntPtr world_ptr);
#endif


    }
}


