using System.Collections.Generic; 
using UnityEngine;
using LitJson;
 namespace core
{
	public class skill_config
	{
		public uint id { get; set; }
		public string hurt_set;
		static Dictionary<uint,skill_config> _Dic; 
		public static Dictionary<uint,skill_config> Dic 
		{
			get
			{
				if(_Dic == null)
				{
					_Dic = new Dictionary<uint,skill_config>();
					string text =  ResourceManager.Instance.Load<TextAsset>("Config/skill_config", "txt").text;
					string[] config_str = text.Split('\n');
					foreach(var str in config_str)
					{
						var config = JsonMapper.ToObject<skill_config>(str);
						if(null != config)
							_Dic[config.id] = config;
					}
				}
				return _Dic;
			}
		}
	}
}
