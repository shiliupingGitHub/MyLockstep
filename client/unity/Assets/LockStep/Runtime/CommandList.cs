
using System.Collections.Generic;
namespace lockstep
{
    public class CommandList : System.IDisposable
    {
        public List<Command> Cmds { get; set; } = new List<Command>();

        public void Dispose()
        {
            Cmds.Clear();
            core.ObjectPool.Instance.Recycle(this);
        }

        public Command[] ToArray()
        {
            return Cmds.ToArray();
        }
    }
}
