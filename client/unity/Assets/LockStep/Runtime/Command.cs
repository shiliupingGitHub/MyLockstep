using System.Runtime.InteropServices;

namespace lockstep
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Command
    {
        public string op;// 操作
        public ulong id;//角色id
        public string data;//操作数据
    }
}
