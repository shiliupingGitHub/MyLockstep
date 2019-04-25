using lockstep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace hotfix
{
    public partial class BattleFrame
    {
        public void OnInit()
        {
            btn_skill.onClick.AddListener(() =>
            {
                UseSkillComand cmd = new UseSkillComand();
                cmd.skill_id = 0;
                ComandManager.Instance.SendCmd(EntityManager.Instance.MyRoleId, cmd);
            });
        }

        void Dispose()
        {
            ObjectPool.Instance.Recycle(this);
        }
    }
}


