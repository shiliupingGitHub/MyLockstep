#include "config_manager.h"
namespace lockstep
{
	ConfigManager& ConfigManager::GetInstance()
	{
		static ConfigManager s_ConfigManager;
		return s_ConfigManager;
	}
	ConfigManager::~ConfigManager()
	{
		for (auto it : mConfigs)
		{		
			delete it.second;
		}
	}
	void ConfigManager::Load()
	{
		for (auto it : mConfigs)
		{
			it.second->Load();
		}
	}

	void ConfigManager::Add(std::string name, IConfig* pConfig)
	{
		mConfigs[name] = pConfig;
	}

	IConfig* ConfigManager::Get(std::string name)
	{
		auto it = mConfigs.find(name);

		if (it != mConfigs.end())
		{
			return it->second;
		}

		return nullptr;
	}
}