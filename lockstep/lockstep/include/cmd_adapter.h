#ifndef LOCKSTEP_CMD_ADAPTER_H
#define LOCKSTEP_CMD_ADAPTER_H
#include <map>
#include "data.h"
using namespace std;
namespace lockstep
{
	class CmdHandler;
	 class World;
	class CmdAdapter
	{

		friend class World;

	private:
		CmdAdapter() = default;
		~CmdAdapter() ;
		CmdAdapter(CmdAdapter&) = delete;
		CmdAdapter& operator =(const CmdAdapter&) = delete;
	public:
		static	CmdAdapter* GetInstance();

		void AddHandler(string, CmdHandler*);
	private:

		void do_cmd(World* pWorld, command_data cmd);

	private:

		std::map<string, CmdHandler*> mHandler;
	};
}



#endif // !_CMD_ADAPTER_H

