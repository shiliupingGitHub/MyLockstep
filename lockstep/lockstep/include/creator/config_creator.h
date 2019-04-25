#ifndef LOCKSTEP_CONFIG_CREATOR
#define LOCKSTEP_CONFIG_CREATOR
#include "config_manager.h"
#include <string>
namespace lockstep
{
	template<class T>
	class ConfigCreator
	{
	public :
		ConfigCreator(std::string name)
		{

			ConfigManager::GetInstance().Add(name, new T());
		}
	};
}
#endif // !_CONFIG_CREATOR

