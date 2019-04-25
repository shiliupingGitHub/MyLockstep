using System;
using System.Runtime.InteropServices;

namespace lockstep
{

    [StructLayout(LayoutKind.Sequential)]
    public  struct EntityData
    {

        public ulong id; //角色ID
        public Vector2 pos;
        public int d;//出生的朝向
        public int camp;//阵营
        public int config_id;// 角色配置数据
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public int[] skills;
        public int skill_size;
    }
    [StructLayout(LayoutKind.Sequential)]
    public  struct LevelData
    {
        public int level_id;
        public uint seed;
  
    }

    [StructLayout(LayoutKind.Sequential)]
    public  struct Vector2
    {
        public int x; //x坐标
        public int y; // y 坐标

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public  struct HurtNum
    {
        int hurt;
    }
    [StructLayout(LayoutKind.Sequential)]
    public class Bound
    {
        public Vector2 size = new Vector2();
        public Vector2 center = new Vector2();
    }

}
