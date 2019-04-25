#include "config/ani_config.h"
#include "controller.h"
#include "rapidjson/document.h"
#include "config_manager.h"
using namespace rapidjson;
namespace lockstep
{
	IMPLEMENT_CONFIG(ani_config);
	ani_config_data* ani_config::Get(unsigned int id)
	{
		auto it = mData.find(id);
		if (it != mData.end())
		{
			return &it->second;
		}
		return nullptr;
	}
	void ani_config::Load()
	{
		mData.clear();
		string content =  Controller::GetInstance()->InvokeReadFile(CONFIG_FILE, 0, "ani_config");
		vector<string> config_str = split(content, "\n");
		for (auto it = config_str.begin(); it != config_str.end(); it++)
		{
			Document d;
			d.Parse((*it).c_str());
			ani_config_data data;
			if (d.HasMember("id"))
			{
				data.id = d["id"].GetUint();
			}
			if (d.HasMember("distance"))
			{
				data.distance = d["distance"].GetInt();
			}
			if (d.HasMember("frozen"))
			{
				data.frozen = d["frozen"].GetInt();
			}
			if (d.HasMember("y_distance"))
			{
				data.y_distance = d["y_distance"].GetInt();
			}
			mData[data.id] = data;
		}
	}
}
