#ifndef LOCKSTEP_ANI_CONFIG_H
#define LOCKSTEP_ANI_CONFIG_H
#include <string>
#include "config/i_config.h"
#include<map>
#include "lockstep.h"
using namespace std;
namespace lockstep
{
	struct ani_config_data 
	{
		unsigned int id;
		int distance;
		int frozen;
		int y_distance;
	};
	class ani_config : public IConfig
	{
	public:
		virtual void Load();
		ani_config_data* Get(unsigned int id);
	private:
		map<unsigned int, ani_config_data> mData;
	};
}
#endif
