using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  core;
namespace lockstep
{
    public class ComandManager :SingleTon<ComandManager>
    {

        public void SendCmd(ulong id, BaseComand cmd)
        {
            if(!EntityManager.Instance.IsLockStep)
            {
                Command opCmd = new Command();
                opCmd.id = id;
                opCmd.op = cmd.GetOp();
                opCmd.data = cmd.Serilize();

                EntityManager.Instance.AddCmd(EntityManager.Instance.CurFrame + 1, opCmd);
            }
        }
    }
}
