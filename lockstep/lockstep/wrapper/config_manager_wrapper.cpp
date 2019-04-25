#include "wrapper_define.h"
#include "config_manager.h"
using namespace lockstep;
extern "C"
{
	_DLL_EXPORT void ConfigManagerLoad()
	{
		ConfigManager::GetInstance().Load();
	}
}