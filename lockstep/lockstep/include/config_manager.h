#ifndef LOCKSTEP_CONFIG_MANAGER_H
#define LOCKSTEP_CONFIG_MANAGER_H
#include <map>
#include "config/i_config.h"
#include <string>
namespace lockstep
{
	class ConfigManager
	{
	private:
		ConfigManager() = default;
		ConfigManager(ConfigManager&) = delete;
		ConfigManager& operator=(const ConfigManager&) = delete;
	public:
		static ConfigManager& GetInstance();
		~ConfigManager();
		void Load();

		void Add(std::string name, IConfig* pConfig);
		IConfig* Get(std::string name);
	private:
		std::map<std::string, IConfig*> mConfigs;
	};


}
#endif // !_CONFIG_MANAGER_H

