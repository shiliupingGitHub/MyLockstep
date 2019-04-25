#include "cmd_adapter.h"
#include "cmd_handler.h"
#include "world.h"
#include "cmd_handlers.h"
using namespace lockstep;

void CmdAdapter::AddHandler(string name, CmdHandler* handler)
{
	mHandler[name] = handler;
}
CmdAdapter* CmdAdapter::GetInstance()
{
	static CmdAdapter s_instance;

	return &s_instance;
}
CmdAdapter::~CmdAdapter()
{

}
void CmdAdapter::do_cmd(World* pWorld, command_data cmd)
{
	string op = cmd.op;

	auto it = mHandler.find(op);

	if (it != mHandler.end())
	{	
		auto pEntity = pWorld->FindEntity(cmd.id);

		pEntity->SetIsCmdCtr(true);
		it->second->handle(pEntity, cmd.data);
	}
}

