using UnityEngine;
using core;
using UnityEngine.UI;
namespace hotfix
{
	[HotfixType("BattleFrame")]
	public partial class BattleFrame
	{
		 ReferenceCollector mRc;
		protected Button btn_skill;
		 public  void OnBind(GameObject go)
		{
			 mRc = go.GetComponent<ReferenceCollector>();
			 btn_skill = mRc.Get<GameObject>("btn_skill").GetComponent<Button>();
		}
	}
}
