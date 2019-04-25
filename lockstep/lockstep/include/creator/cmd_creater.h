#ifndef LOCKSTEP_CMD_CREATOR_H
#define LOCKSTEP_CMD_CREATOR_H
#include "cmd_adapter.h"
#include <string>
namespace lockstep
{

	class CmdHandler;
;
	template<class T>
	class CmdCreator 
	{
	public:
		CmdCreator(std::string name)
		{
			CmdAdapter::GetInstance()->AddHandler(name, new T());
		}
	};
}


#endif // !_CMD_CREATOR_H

